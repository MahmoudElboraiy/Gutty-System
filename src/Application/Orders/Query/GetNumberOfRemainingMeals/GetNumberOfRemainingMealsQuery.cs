
using MediatR;

namespace Application.Orders.Query.GetNumberOfRemainingMeals;

public record GetNumberOfRemainingMealsQuery() : IRequest<GetNumberOfRemainingMealsQueryResponse>;
public record GetNumberOfRemainingMealsQueryResponse(List<RemainingMealsItem> RemainingMeals);
public record RemainingMealsItem(int SubCategoryId, int RemainingMeals);