
using MediatR;

namespace Application.Orders.Query.ShowOrderDetails;

public record ShowOrderDetailsQuery(int OrderId) : IRequest<ShowOrderDetailsQueryResponse>;
public record ShowOrderDetailsQueryResponse(List<ShowOrderMealsDetailsQueryResponseItem> OrderMeals);
public record ShowOrderMealsDetailsQueryResponseItem(
    int OrderMealId,
    string MealName,
    string ImageUrl,
    int? MealId,
    int? ProteinMealId,
    int? CarbMealId,
    bool AcceptCarb,
    string? Notes
);
