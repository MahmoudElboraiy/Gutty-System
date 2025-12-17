

using ErrorOr;
using MediatR;

namespace Application.Ingredients.Queries.GetIngredientById;

public record GetIngredientByIdQuery(int Id) :
    IRequest<ErrorOr<GetIngredientByIdQueryResponse>>;
public record GetIngredientByIdQueryResponse(
    int Id,
    string Name,
    decimal CaloriesPer100g,
    decimal ProteinPer100g,
    decimal CarbsPer100g,
    decimal FatsPer100g
    );

