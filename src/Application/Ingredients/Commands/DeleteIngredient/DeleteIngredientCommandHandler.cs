
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.DeleteIngredient;

public class DeleteIngredientCommandHandler :
    IRequestHandler<DeleteIngredientCommand, ErrorOr<DeleteIngredientCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteIngredientCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<DeleteIngredientCommandResponse>> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(request.Id);
        if (ingredient is null)
        {
            return Error.NotFound(description: $"Ingredient with id {request.Id} not found.");
        }
        _unitOfWork.Ingredients.Remove(ingredient);
        await _unitOfWork.CompleteAsync();
        var response = new DeleteIngredientCommandResponse(
            true,
            $"Ingredient with id {request.Id} deleted successfully."
            );
        return response;
    }
}
