using ErrorOr;
using MediatR;

namespace Application.Meals.Query.GetMealDetails;

public record GetMealDetailsQuery(int MealId,string UserId) : IRequest<ErrorOr<GetMealDetailsQueryResponse>>;
public record GetMealDetailsQueryResponse(
    int Id,
    string Name,
    string Description,
    string ImageUrl,
    decimal Calories,
    decimal Protein,
    decimal Carbs,
    decimal Fats,
    int CategoryId,
    int SubcategoryId,
    decimal? DefaultQuantityGrams
    );
