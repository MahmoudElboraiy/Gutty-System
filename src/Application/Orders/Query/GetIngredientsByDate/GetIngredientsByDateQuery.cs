
using Domain.Enums;
using MediatR;

namespace Application.Orders.Query.GetIngredientsByDate;

public record GetIngredientsByDateQuery(DateOnly DeliveryDate)
    : IRequest<GetIngredientsByDateQueryResponse>;
public record GetIngredientsByDateQueryResponse(
    DateOnly DeliveryDate,
    List<IngredientQuantity> Ingredients,
    List<MealsQuantity> Meals
);

public record IngredientQuantity(string IngredientName, decimal TotalGrams);
public record MealsQuantity(string MealName, decimal TotalCount);
