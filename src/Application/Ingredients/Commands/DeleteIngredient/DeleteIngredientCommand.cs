

using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.DeleteIngredient;

public record DeleteIngredientCommand(
    int Id
    ) : IRequest<ErrorOr<DeleteIngredientCommandResponse>>;
public record DeleteIngredientCommandResponse(
    bool Success,
    string Message
    );

