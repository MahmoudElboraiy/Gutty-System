using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Meals.Queries.GetMeal;

public record GetMealQuery(Guid Id) : IRequest<ErrorOr<GetMealQueryResponse>>;

public record GetMealQueryResponse(
    Guid Id,
    Guid ItemId,
    string ItemName,
    string ItemNameAr,
    decimal Weight,
    decimal Price,
    MealType MealType,
    uint Quantity,
    DateTime CreatedAt
);