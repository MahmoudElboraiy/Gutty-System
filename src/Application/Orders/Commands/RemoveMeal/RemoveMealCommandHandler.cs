

using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.RemoveMeal;

public class RemoveMealCommandHandler : IRequestHandler<RemoveMealCommand, RemoveMealCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public RemoveMealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<RemoveMealCommandResponse> Handle(RemoveMealCommand request, CancellationToken cancellationToken)
    {
       // var userId = _currentUserService.UserId;
          
       // var subscription = await _unitOfWork.Subscriptions.GetQueryable()
        //    .Include(s => s.LunchCategories)
         //   .FirstOrDefaultAsync(s => s.UserId == userId && s.IsCurrent && !s.IsPaused, cancellationToken);

       // if (subscription == null)
         //   return new RemoveMealCommandResponse(false, "No active subscription found.");

        var orderMeal = await _unitOfWork.OrderMeals
            .GetQueryable()
            .Include(o => o.Order)
                .ThenInclude(o => o.Subscription)
                    .ThenInclude(s => s.LunchCategories)
            .FirstOrDefaultAsync(o =>o.Id == request.orderMealId); 

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

      //  var subCategory = subscription.LunchCategories
         //   .FirstOrDefault(c => c.SubCategoryId == meal.SubcategoryId);

       var subCategory = orderMeal.Order.Subscription.LunchCategories
            .FirstOrDefault(c => c.SubCategoryId == meal.SubcategoryId);

        //if (mealType == MealType.Protien && subCategory != null)
        //{
        //    subscription.LunchMealsLeft++;
        //    subCategory.NumberOfMealsLeft++;
        //}
        if (mealType == MealType.Protien && subCategory != null)
        {
            orderMeal.Order.Subscription.LunchMealsLeft++;
            subCategory.NumberOfMealsLeft++;
        }
        if (orderMeal.Order.Subscription.IsCurrent==false)
        {
            orderMeal.Order.Subscription.IsCurrent = true;
        }
        if(orderMeal.Order.IsCompleted)
        {
            orderMeal.Order.Subscription.DaysLeft += (uint)orderMeal.Order.DayNumber;
            orderMeal.Order.IsCompleted = false;
        }
        _unitOfWork.OrderMeals.Remove(orderMeal);
        await _unitOfWork.CompleteAsync();
       return new RemoveMealCommandResponse(true, "Meal removed successfully from the order.");
    }
}
