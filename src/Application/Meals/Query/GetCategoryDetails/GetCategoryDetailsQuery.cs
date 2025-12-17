

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Meals.Query.GetCategoryDetails;

public record GetCategoryDetailsQuery(
    int Id
) : IRequest<ErrorOr<GetCategoryDetailsQueryResponse>>;

public record GetCategoryDetailsQueryResponse(
    int Id,
    string Name,
    MealType MealType
);
