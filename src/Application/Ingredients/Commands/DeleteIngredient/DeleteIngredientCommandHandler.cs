using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.DeleteIngredient;

public class DeleteIngredientCommandHandler
    : IRequestHandler<DeleteIngredientCommand, ErrorOr<DeleteIngredientCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIngredientRepository _ingredientRepository;

    public DeleteIngredientCommandHandler(
        IUnitOfWork unitOfWork,
        IIngredientRepository ingredientRepository
    )
    {
        _unitOfWork = unitOfWork;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<ErrorOr<DeleteIngredientCommandResponse>> Handle(
        DeleteIngredientCommand request,
        CancellationToken cancellationToken
    )
    {
        var ingredient = await _ingredientRepository.GetAsync(request.Id);

        if (ingredient == null)
        {
            return DomainErrors.Ingredients.IngredientNotFound(request.Id);
        }

        await _ingredientRepository.DeleteAsync(ingredient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new DeleteIngredientCommandResponse();
    }
}
