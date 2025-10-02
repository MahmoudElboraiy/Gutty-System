

using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.RemoveMeal;

public class RemoveMealCommandHandler : IRequestHandler<RemoveMealCommand, RemoveMealCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public RemoveMealCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<RemoveMealCommandResponse> Handle(RemoveMealCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var subscription = await _unitOfWork.Subscriptions.GetQueryable()
            .Include(s => s.LunchCategories)
            .FirstOrDefaultAsync(s => s.UserId == userId && s.IsCurrent && !s.IsPaused, cancellationToken);

        if (subscription == null)
            return new RemoveMealCommandResponse(false, "No active subscription found.");

        var orderMeal = await _unitOfWork.OrderMeals.GetByIdAsync(request.orderMealId); 
        if (orderMeal == null)
        {
            return new RemoveMealCommandResponse (false,"Meal not found in the order.");
        }
        var meal = await _unitOfWork.Meals.GetQueryable()
            .AsNoTracking()
            .Where(m => m.Id == request.mealId)
            .Select(m => new { m.MealType, m.SubcategoryId })
            .FirstOrDefaultAsync(cancellationToken);

        if (meal == null)
            return new RemoveMealCommandResponse(false, "Meal not found.");

        var mealType = meal?.MealType;

        var subCategory = subscription.LunchCategories
            .FirstOrDefault(c => c.SubCategoryId == meal.SubcategoryId);
       

        if (mealType == MealType.Protien && subCategory != null)
        {
            subscription.LunchMealsLeft++;
            subCategory.NumberOfMealsLeft++;
        }
        _unitOfWork.OrderMeals.Remove(orderMeal);
        await _unitOfWork.CompleteAsync();
       return new RemoveMealCommandResponse(true, "Meal removed successfully from the order.");
    }
}
