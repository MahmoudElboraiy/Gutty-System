using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.DeleteIngredient;

public class DeleteIngredientCommandHandler
    : IRequestHandler<DeleteIngredientCommand, ErrorOr<DeleteIngredientCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteIngredientCommandHandler(
        IUnitOfWork unitOfWork
    )
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<DeleteIngredientCommandResponse>> Handle(
        DeleteIngredientCommand request,
        CancellationToken cancellationToken
    )
    {
        var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(request.Id);

        if (ingredient == null)
        {
            return DomainErrors.Ingredients.IngredientNotFound(request.Id);
        }

        _unitOfWork.Ingredients.Remove(ingredient);

        await _unitOfWork.CompleteAsync();

        return new DeleteIngredientCommandResponse();
    }
}
