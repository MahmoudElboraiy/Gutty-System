
using MediatR;

namespace Application.Orders.Query.GetIngredientsByDate;

public record GetIngredientsByDateQuery(DateOnly DeliveryDate)
    : IRequest<GetIngredientsByDateQueryResponse>;
public record GetIngredientsByDateQueryResponse(
    DateOnly DeliveryDate,
    List<IngredientQuantity> Ingredients
);

public record IngredientQuantity(string IngredientName, decimal TotalGrams);
