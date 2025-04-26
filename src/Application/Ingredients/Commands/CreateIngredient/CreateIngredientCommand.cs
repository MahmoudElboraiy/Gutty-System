using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.CreateIngredient;

public record CreateIngredientCommand(string Name,string Description = "", decimal Stock = 0)
    : IRequest<ErrorOr<CreateIngredientCommandResponse>>;
