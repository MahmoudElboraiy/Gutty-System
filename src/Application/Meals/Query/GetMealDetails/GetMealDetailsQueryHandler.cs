

using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Meals.Query.GetMealDetails;

public class GetMealDetailsQueryHandler : IRequestHandler<GetMealDetailsQuery, GetMealDetailsQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetMealDetailsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetMealDetailsQueryResponse> Handle(GetMealDetailsQuery request, CancellationToken cancellationToken)
    {
        var meal = await _unitOfWork.Meals
            .GetQueryable()
            .Where(m => m.Id == request.MealId)
            .AsNoTracking()
            .Include(i=>i.Ingredient)
            .FirstOrDefaultAsync(cancellationToken);
        GetMealDetailsQueryResponse responseItem = null;
        if (meal.IngredientId == null)
        {
            responseItem = new GetMealDetailsQueryResponse
            (
                meal.Id,
                meal.Name,
                meal.Description,
                meal.ImageUrl,
                meal.FixedCalories.Value,
                meal.FixedProtein.Value,
                meal.FixedCarbs.Value,
                meal.FixedFats.Value,
                meal.DefaultQuantityGrams
            );
            return responseItem;
        }

        var subscription = _unitOfWork.Subscriptions
            .GetQueryable()
            .Where(s => s.UserId == request.UserId && s.IsActive)
            .Include(s => s.LunchCategories)
            .AsNoTracking()
            .FirstOrDefault();
        decimal ingredient =100;
        var subscriptionCategory = subscription?.LunchCategories
                .FirstOrDefault(lc => lc.SubCategoryId == meal.SubcategoryId);
        if (subscriptionCategory == null || meal.MealType == null)
        {
            ingredient = meal.DefaultQuantityGrams ?? 100;
        }
        else if(meal.MealType == MealType.Protien)
        {  
            ingredient = subscriptionCategory.ProteinGrams;
        }else if(meal.MealType ==MealType.Carb_Rice)
            ingredient =subscription.RiceCarbGrams;
        else if (meal.MealType == MealType.Carb_Pasta)
            ingredient = subscription.PastaCarbGrams;
        decimal ratio = ingredient / 100;
         responseItem =new GetMealDetailsQueryResponse
        (
            meal.Id,
            meal.Name,
            meal.Description,
            meal.ImageUrl,
            meal.Ingredient.CaloriesPer100g * ratio,
            meal.Ingredient.ProteinPer100g * ratio,
            meal.Ingredient.CarbsPer100g * ratio,
            meal.Ingredient.FatsPer100g * ratio,
            ingredient
        );
        return responseItem;
    }
}
