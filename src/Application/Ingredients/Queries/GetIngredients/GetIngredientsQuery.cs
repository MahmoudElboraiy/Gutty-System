

using ErrorOr;
using MediatR;

namespace Application.Ingredients.Queries.GetIngredients;

public record GetIngredientsQuery(int pageNumber,int pageSize) :
    IRequest<ErrorOr<List<GetIngredientsQueryResponse>>>;
public record GetIngredientsQueryResponse(
    int Id,
    string Name,
    decimal CaloriesPer100g,
    decimal ProteinPer100g,
    decimal CarbsPer100g,
    decimal FatsPer100g
    );

