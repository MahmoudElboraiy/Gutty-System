
using Domain.Enums;
using MediatR;

namespace Application.SubCategories.Query.GetBreakFastAndDinner;

public record GetSubCategoriesByTypeQuery(MealType MealType):
    IRequest<GetSubCategoriesByTypeQueryResponse>;
public record GetSubCategoriesByTypeQueryResponse(List<GetSubCategoriesByTypeQueryResponseItem> SubCategories);
public record GetSubCategoriesByTypeQueryResponseItem(
    int Id,
    string Name,
    int CategoryId
    );