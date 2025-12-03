

using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.CreateIngredient;

public record CreateIngredientCommand(
    string Name,
    decimal CaloriesPer100g,
    decimal ProteinPer100g,
    decimal CarbsPer100g,
    decimal FatsPer100g
    ) : IRequest<ErrorOr<CreateIngredientCommandResponse>>;
public record CreateIngredientCommandResponse(int Id);
