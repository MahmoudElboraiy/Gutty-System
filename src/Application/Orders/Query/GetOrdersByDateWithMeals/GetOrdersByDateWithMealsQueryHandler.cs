
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vonage.Voice;

namespace Application.Orders.Query.GetOrdersByDateWithMeals;

public class GetOrdersByDateWithNutritionQueryHandler
    : IRequestHandler<GetOrdersByDateWithNutritionQuery, ErrorOr<List<OrderWithNutritionResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersByDateWithNutritionQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<List<OrderWithNutritionResponse>>> Handle(
        GetOrdersByDateWithNutritionQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders
            .GetQueryable()
            .AsNoTracking()
            .Where(o => o.DeliveryDate == request.DeliveryDate)
            .Include(o => o.Subscription)
                .ThenInclude(s => s.User)
            .Include(o => o.Subscription)
                .ThenInclude(s => s.LunchCategories)
            .Include(o => o.Meals)
                .ThenInclude(om => om.Meal)
                    .ThenInclude(m => m.Ingredient)
            .Include(o => o.Meals)
                .ThenInclude(om => om.ProteinMeal)
                    .ThenInclude(m => m.Ingredient)
            .Include(o => o.Meals)
                .ThenInclude(om => om.CarbMeal)
                    .ThenInclude(m => m.Ingredient)
            .ToListAsync(cancellationToken);

        var result = new List<OrderWithNutritionResponse>();

        foreach (var order in orders)
        {
            var subscription = order.Subscription;
            var user = subscription.User;

            var orderResponse = new OrderWithNutritionResponse
            {
                OrderId = order.Id,
                CustomerName = user?.Name,
                PhoneNumber = user?.PhoneNumber ?? "N/A",
                Address = user?.MainAddress ?? "N/A"
            };

            var breakfastAndDinner = new List<MealWithNutritionResponse>();

            foreach (var om in order.Meals)
            {
                // 🍳 Breakfast & Dinner
                if (om.MealType == MealType.BreakFastAndDinner)
                {
                    var meal = om.Meal;
                    if (meal == null) continue;

                    var ingr = meal.Ingredient;
                    decimal grams = meal.DefaultQuantityGrams ?? 100;
                    decimal ratio = grams / 100;

                    breakfastAndDinner.Add(new MealWithNutritionResponse
                    {
                        MealName = meal.Name,
                        Quantity = grams,
                        Notes = om.Notes,
                        Nutrition = new NutritionResponse
                        {
                            Calories = ingr?.CaloriesPer100g * ratio ?? meal.FixedCalories ?? 0,
                            Protein = ingr?.ProteinPer100g * ratio ?? meal.FixedProtein ?? 0,
                            Carbs = ingr?.CarbsPer100g * ratio ?? meal.FixedCarbs ?? 0,
                            Fats = ingr?.FatsPer100g * ratio ?? meal.FixedFats ?? 0
                        }
                    });
                }

                // 🍱 Lunch (Protein + Carb)
                else if (om.MealType == MealType.Protien)
                {
                    var proteinMeal = om.ProteinMeal;
                    var carbMeal = om.CarbMeal;

                    if (proteinMeal != null)
                    {
                        var proteinIngr = proteinMeal.Ingredient;
                        var category = subscription?.LunchCategories
                            .FirstOrDefault(c => c.SubCategoryId == proteinMeal.SubcategoryId);

                        decimal grams = category?.ProteinGrams ?? proteinMeal.DefaultQuantityGrams ?? 100;
                        decimal ratio = grams / 100;

                        var lunchProtein = new LunchProteinWithCarbResponse
                        {
                            ProteinName = proteinMeal.Name,
                            ProteinQuantity = grams,
                            Notes = om.Notes,
                            ProteinNutrition = new NutritionResponse
                            {
                                Calories = proteinIngr?.CaloriesPer100g * ratio ?? proteinMeal.FixedCalories ?? 0,
                                Protein = proteinIngr?.ProteinPer100g * ratio ?? proteinMeal.FixedProtein ?? 0,
                                Carbs = proteinIngr?.CarbsPer100g * ratio ?? proteinMeal.FixedCarbs ?? 0,
                                Fats = proteinIngr?.FatsPer100g * ratio ?? proteinMeal.FixedFats ?? 0
                            }
                        };

                        // 🔗 Linked carb (if exists)
                        if (carbMeal != null)
                        {
                            var carbIngr = carbMeal.Ingredient;
                            decimal carbGrams = subscription?.CarbGrams ?? carbMeal.DefaultQuantityGrams ?? 100;
                            decimal carbRatio = carbGrams / 100;

                            lunchProtein.Carb = new CarbResponse
                            {
                                CarbName = carbMeal.Name,
                                CarbQuantity = carbGrams,
                                Notes = om.Notes,
                                CarbNutrition = new NutritionResponse
                                {
                                    Calories = carbIngr?.CaloriesPer100g * carbRatio ?? carbMeal.FixedCalories ?? 0,
                                    Protein = carbIngr?.ProteinPer100g * carbRatio ?? carbMeal.FixedProtein ?? 0,
                                    Carbs = carbIngr?.CarbsPer100g * carbRatio ?? carbMeal.FixedCarbs ?? 0,
                                    Fats = carbIngr?.FatsPer100g * carbRatio ?? carbMeal.FixedFats ?? 0
                                }
                            };
                        }

                        orderResponse.Lunch.Add(lunchProtein);
                    }
                }
            }

            // ✅ Group breakfast & dinner by meal name and count
            orderResponse.BreakfastAndDinner = breakfastAndDinner
                .GroupBy(m => new { m.MealName, m.Quantity, m.Notes })
                .Select(g => new MealWithNutritionResponse
                {
                    MealName = g.Key.MealName,
                    Quantity = g.Key.Quantity,
                    Notes = g.Key.Notes,
                    Count = g.Count(),
                    Nutrition = g.First().Nutrition
                })
                .ToList();

            result.Add(orderResponse);
        }

        return result;
    }
}