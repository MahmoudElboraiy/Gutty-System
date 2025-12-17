using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.AddMeal;

public class AddMealCommandHandler : IRequestHandler<AddMealCommand, AddMealCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public AddMealCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<AddMealCommandResponse> Handle(AddMealCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var subscription = await _unitOfWork.Subscriptions.GetQueryable()
            .Include(s => s.LunchCategories)
            .Include(s => s.Plan)
            .FirstOrDefaultAsync(s => s.UserId == userId && s.IsCurrent && !s.IsPaused, cancellationToken);

        if(subscription == null)
        {
            return new AddMealCommandResponse(false, "No active subscription found for the user.");
        }
        var order = await _unitOfWork.Orders.GetQueryable()
            .Where(o => o.SubscriptionId == subscription.Id && !o.IsCompleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (order == null)
        {
            return new AddMealCommandResponse(false, "Order not found.");
        }

        var meal =await _unitOfWork.Meals.GetQueryable()
            .AsNoTracking()
            .Where(m => m.Id == request.MealId)
            .Select(m =>new { m.MealType, m.SubcategoryId })
            .FirstOrDefaultAsync(cancellationToken);

        if(meal == null)
        {
            return new AddMealCommandResponse(false, "Meal not found.");
        }

        var mealType = meal?.MealType;

            var subCategoryId = meal!.SubcategoryId;

        var subscriptionCategory = subscription?.LunchCategories
               .FirstOrDefault(c => c.SubCategoryId == subCategoryId);

       var planId = subscription?.PlanId;

        //var mealsPerDa2 = await _unitOfWork.Plans.GetQueryable()
        //      .Where(p => p.Id == planId)
        //      .Select(p => new { p.LMealsPerDay ,p.BDMealsPerDay})
        //      .FirstOrDefaultAsync(cancellationToken);

        //var mealCount = await _unitOfWork.OrderMeals
        //    .GetQueryable()
        //    .AsNoTracking()
        //    .Where(m => m.OrderId == request.orderId)
        //    .CountAsync(m => m.MealType == mealType);
        var mealCount =order.Meals.Count(m => m.MealType == mealType);
        var mealsPerDay = new { subscription.Plan.LMealsPerDay, subscription.Plan.BDMealsPerDay };

        var BD_OrderCount = (order!.DayNumber * mealsPerDay!.BDMealsPerDay);
        var L_OrderCount = (order!.DayNumber * mealsPerDay!.LMealsPerDay);

        if (mealType == MealType.BreakFastAndDinner
                && request.count + mealCount > BD_OrderCount)
        { 
            return new AddMealCommandResponse(false, "Exceeded daily breakfast/dinner limit");
        }
        if (mealType == MealType.Protien)
        {
            if (subscriptionCategory == null ||
                subscriptionCategory.NumberOfMealsLeft < request.count ||
                request.count + mealCount > L_OrderCount)
            {
                return new AddMealCommandResponse(false, "Not enough protein meals left in your subscription");
            }

            subscriptionCategory.NumberOfMealsLeft -= (uint)request.count;
            subscription!.LunchMealsLeft -= (uint)request.count;
        }
        for (int i = 0; i < request.count; i++)
        {
            var orderMeal = new OrderMeal
            {
                OrderId = order.Id,
                MealType = (MealType)mealType!,
                Notes = request.Notes
            };

            if (meal.MealType == MealType.BreakFastAndDinner)
                orderMeal.MealId = request.MealId;
            else if (meal.MealType == MealType.Protien)
                orderMeal.ProteinMealId = request.MealId;

            order.Meals.Add(orderMeal);  
        }
     
        await _unitOfWork.CompleteAsync();
        return new AddMealCommandResponse(true, "The Meal had be Added Successfuly");
    }
}
