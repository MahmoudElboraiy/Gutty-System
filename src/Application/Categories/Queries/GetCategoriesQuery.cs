

using Domain.Enums;
using MediatR;

namespace Application.Categories.Queries;

public record GetCategoriesQuery () : IRequest<GetCategoriesQueryResponse>;
public record  GetCategoriesQueryResponse(List<GetCategoryQueryResponseItem> Categories);

public record GetCategoryQueryResponseItem (
    int Id,
    string Name,
    MealType Type
    );