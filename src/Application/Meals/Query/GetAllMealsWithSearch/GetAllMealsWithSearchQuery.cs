

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Meals.Query.GetAllMealsWithSearch;

public record GetAllMealsWithSearchQuery(
    int PageNumber,
    int PageSize,
    string? searchName,
    string? SubCategoryName
) : IRequest<ErrorOr<GetAllMealsWithSearchQueryResponse>>;
public record GetAllMealsWithSearchQueryResponse(
    int pageNumber,
    int pageSize,
    int TotalCount,
    List<GetAllMealsWithSearchItem> Meals);
public record GetAllMealsWithSearchItem(
    int Id ,
 string Name ,
 string Description ,
 string ImageUrl ,
 decimal Calories ,
 decimal Protein ,
 decimal Carbs ,
 decimal Fats,
 MealType? MealType,
 string SubcategoryName,
 decimal? DefaultQuantityGrams
);

