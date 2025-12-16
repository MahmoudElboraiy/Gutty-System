

using ErrorOr;
using MediatR;

namespace Application.Ingredients.Queries.GetIngredients;

public record GetIngredientsQuery(int pageNumber,int pageSize,string? searchName) :
    IRequest<ErrorOr<GetIngredientsQueryResponse>>;
public record GetIngredientsQueryResponse(
    int pageNumber,
    int pageSize,
    int TotalCount,
    List<GetIngredientsItem> Ingredients
    );
public record class GetIngredientsItem(
    int Id,
    string Name,
    decimal CaloriesPer100g,
    decimal ProteinPer100g,
    decimal CarbsPer100g,
    decimal FatsPer100g
    );

