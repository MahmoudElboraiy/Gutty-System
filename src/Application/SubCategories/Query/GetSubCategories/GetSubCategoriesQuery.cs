
using MediatR;

namespace Application.SubCategories.Query.GetSubCategories;

public record GetSubCategoriesQuery(int CategoryId) : IRequest<GetSubCategoriesQueryResponse>;
public record GetSubCategoriesQueryResponse(List<GetSubCategoryQueryResponseItem> SubCategories);
public record GetSubCategoryQueryResponseItem(
    int Id,
    string Name,
    int CategoryId
    );
