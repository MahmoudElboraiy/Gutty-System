

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
            }
        }

        // تحويل النتيجة إلى شكل منظم
        var resultList = totalIngredients
            .Select(i => new IngredientQuantity(i.Key, i.Value))
            .ToList();

        return new GetIngredientsByDateQueryResponse(request.DeliveryDate, resultList);
    }
}
