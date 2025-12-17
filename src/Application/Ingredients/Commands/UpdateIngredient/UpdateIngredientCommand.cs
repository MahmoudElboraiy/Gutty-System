
using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.UpdateIngredient;

public record UpdateIngredientCommand(
    int Id,
    string Name,
    decimal CaloriesPer100g,
    decimal ProteinPer100g,
    decimal CarbsPer100g,
    decimal FatsPer100g
    ) : IRequest<ErrorOr<UpdateIngredientCommandResponse>>;
public record UpdateIngredientCommandResponse(int Id);
