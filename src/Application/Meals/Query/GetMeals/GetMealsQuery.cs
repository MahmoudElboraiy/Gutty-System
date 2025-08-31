using MediatR;

namespace Application.Meals.Query.GetMeals;

public record GetMealsQuery(int SubCategoryId) : IRequest<GetMealsQueryResponse>;
public record GetMealsQueryResponse(List<GetMealQueryResponseItem> Meals);
public record GetMealQueryResponseItem(
    int Id,
    string Name,
    string Description,
    string ImageUrl
    );
