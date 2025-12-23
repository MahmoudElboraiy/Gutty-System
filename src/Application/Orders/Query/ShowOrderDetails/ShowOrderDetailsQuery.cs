
using ErrorOr;
using MediatR;

namespace Application.Orders.Query.ShowOrderDetails;

public record ShowOrderDetailsQuery() : IRequest<ErrorOr<ShowOrderDetailsQueryResponse>>;
public record ShowOrderDetailsQueryResponse(List<ShowOrderMealsDetailsQueryResponseItem> OrderMeals
    ,int SelectedBreakFastAndDinner,int AllowBreakFastAndDinnerMeals,int SelectedLunch ,int AllowLunchMeals, bool IsChangeDeliveryDate,bool IsToday);
public record ShowOrderMealsDetailsQueryResponseItem(
    int OrderMealId,
    string MealName,
    string ImageUrl,
    int? BreakFastAndDinnerMealId,
    int? ProteinMealId,
    int? CarbMealId,
    string? CarbMealName,
    bool AcceptCarb,
    string? Notes
);
