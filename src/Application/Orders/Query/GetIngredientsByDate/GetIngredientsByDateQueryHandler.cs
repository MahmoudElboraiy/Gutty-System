

using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Query.GetIngredientsByDate;

public class GetIngredientsByDateQueryHandler
    : IRequestHandler<GetIngredientsByDateQuery, GetIngredientsByDateQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetIngredientsByDateQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetIngredientsByDateQueryResponse> Handle(
        GetIngredientsByDateQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders
            .GetQueryable()
            .AsNoTracking()
            .Where(o => o.DeliveryDate == request.DeliveryDate)
            .Include(o => o.Meals)
                .ThenInclude(om => om.Meal)
                    .ThenInclude(m => m.Ingredient)
            .Include(o => o.Meals)
                .ThenInclude(om => om.ProteinMeal)
                    .ThenInclude(m => m.Ingredient)
            .Include(o => o.Meals)
                .ThenInclude(om => om.CarbMeal)
                    .ThenInclude(m => m.Ingredient)
            .Include(o => o.Subscription)
                .ThenInclude(s => s.LunchCategories)
            .ToListAsync(cancellationToken);


        var totalIngredients = new Dictionary<string, decimal>();
        var mealCounts = new Dictionary<string, int>();

        foreach (var order in orders)
        {
            foreach (var orderMeal in order.Meals)
            {
                var mealsToCheck = new List<Meal?>()
                {
                    orderMeal.Meal,
                    orderMeal.ProteinMeal,
                    orderMeal.CarbMeal
                };

                foreach (var meal in mealsToCheck.Where(m => m != null && m.Ingredient != null))
                {
                    decimal grams = meal.DefaultQuantityGrams ?? 100;

                    var subCategory = order.Subscription.LunchCategories
                        .FirstOrDefault(lc => lc.SubCategoryId == meal.SubcategoryId);

                    if (meal.MealType == MealType.Protien && subCategory != null)
                        grams = subCategory.ProteinGrams;
                    else if (meal.MealType == MealType.Carb && subCategory != null)
                        grams = order.Subscription.CarbGrams;

                    var ingredientName = meal.Ingredient.Name;

                    if (!totalIngredients.ContainsKey(ingredientName))
                        totalIngredients[ingredientName] = 0;

                    totalIngredients[ingredientName] += grams;                    
                }
                //foreach( var meal in mealsToCheck.Where(m=>m!=null))
                //{
                //    var mealKey = meal.Name;
                //    if (!mealCounts.ContainsKey(mealKey))
                //        mealCounts[mealKey] = 0;
                //    mealCounts[mealKey]++;
                //}              
            }
        }
        // Breakfast & Dinner
        foreach (var orderMeal in orders.SelectMany(o => o.Meals))
        {
            if (orderMeal.Meal != null && orderMeal.Meal.MealType == MealType.BreakFastAndDinner)
            {
                var key = orderMeal.Meal.Name;
                if (!mealCounts.ContainsKey(key))
                    mealCounts[key] = 0;
                mealCounts[key]++;
            }
        }

        // Protein
        foreach (var orderMeal in orders.SelectMany(o => o.Meals))
        {
            if (orderMeal.ProteinMeal != null)
            {
                var key = orderMeal.ProteinMeal.Name;
                if (!mealCounts.ContainsKey(key))
                    mealCounts[key] = 0;
                mealCounts[key]++;
            }
        }

        // Carb
        foreach (var orderMeal in orders.SelectMany(o => o.Meals))
        {
            if (orderMeal.CarbMeal != null)
            {
                var key = orderMeal.CarbMeal.Name;
                if (!mealCounts.ContainsKey(key))
                    mealCounts[key] = 0;
                mealCounts[key]++;
            }
        }


        var resultList = totalIngredients
            .Select(i => new IngredientQuantity(i.Key, i.Value))
            .ToList();

        var mealsResult = mealCounts
           .Select(m => new MealsQuantity(
               m.Key,
               m.Value
           ))
           .ToList();

        return new GetIngredientsByDateQueryResponse(request.DeliveryDate, resultList, mealsResult);
    }
}
