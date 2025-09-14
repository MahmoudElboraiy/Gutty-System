using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Meals.Queries.GetMeal;

public class GetMealQueryHandler : IRequestHandler<GetMealQuery, ErrorOr<GetMealQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMealQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<GetMealQueryResponse>> Handle(
        GetMealQuery request,
        CancellationToken cancellationToken)
    {
        var meal = await _unitOfWork
            .Meals.GetQueryable()
            .AsNoTracking()
            .Include(m => m.Item)
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);

        if (meal == null)
        {
            return DomainErrors.Meals.MealNotFound(request.Id);
        }

        return new GetMealQueryResponse(
            meal.Id,
            meal.ItemId,
            meal.Item.Name,
            meal.Item.NameAr,
            meal.Weight,
            meal.Price,
            meal.MealType,
            meal.Quantity,
            meal.CreatedAt
        );
    }
}