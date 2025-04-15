using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.UpdateIngredient;

public record UpdateIngredientCommand(
    int Id,
    string Name,
    decimal Stock
    ) : IRequest<ErrorOr<UpdateIngredientCommandResponse>>;