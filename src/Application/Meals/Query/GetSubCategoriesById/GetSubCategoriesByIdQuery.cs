
using ErrorOr;
using MediatR;

namespace Application.Meals.Query.GetSubCategoriesById;

public record GetSubCategoriesByIdQuery(
    int SubCategoryId
) : IRequest<ErrorOr<GetSubCategoriesByIdQueryResponse>>;
public record GetSubCategoriesByIdQueryResponse(
    int SubCategoryId,
    string SubCategoryName,
    string ImageUrl,
    int CategoryId
);