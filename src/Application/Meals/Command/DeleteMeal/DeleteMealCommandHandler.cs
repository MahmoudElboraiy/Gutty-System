

using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Meals.Command.DeleteMeal;

public class DeleteMealCommandHandler : IRequestHandler<DeleteMealCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    public DeleteMealCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
    {
        var meal = await _unitOfWork.Meals.GetByIdAsync(request.Id);
        if (meal == null)
        {
            return Error.NotFound("Meal.NotFound", "Meal not found");
        }
        var today = DateOnly.FromDateTime(DateTime.Now);

        bool isUsedInOrders = await _unitOfWork.Orders
            .GetQueryable()
            .AsNoTracking()
            .Where(o => o.DeliveryDate >= today)
            .SelectMany(o => o.Meals) 
            .AnyAsync(om => om.MealId == meal.Id
                           || om.ProteinMealId == meal.Id
                           || om.CarbMealId == meal.Id,
                       cancellationToken);
        if (isUsedInOrders)
        {
            return Error.Validation(
                code: "Meal.DeleteError",
                description: $"Cannot delete meal with id {request.Id} because it is used in upcoming orders."
                );
        }
        _cacheService.IncrementVersion(CacheKeys.MealsVersion);
        _unitOfWork.Meals.Remove(meal);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage
        {
            Message = "Meal deleted successfully",
            Success = true
        };
    }
}
