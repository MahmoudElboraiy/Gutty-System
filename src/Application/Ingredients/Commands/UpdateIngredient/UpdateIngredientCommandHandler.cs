using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using Domain.Enums;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.UpdateIngredient;

public class UpdateIngredientCommandHandler
    : IRequestHandler<UpdateIngredientCommand, ErrorOr<UpdateIngredientCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IIngredientLogRepository _ingredientLogRepository;

    public UpdateIngredientCommandHandler(
        IIngredientLogRepository ingredientLogRepository,
        IIngredientRepository ingredientRepository,
        IUnitOfWork unitOfWork
    )
    {
        _ingredientLogRepository = ingredientLogRepository;
        _ingredientRepository = ingredientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<UpdateIngredientCommandResponse>> Handle(
        UpdateIngredientCommand request,
        CancellationToken cancellationToken
    )
    {
        var oldIngredient = await _ingredientRepository.GetAsync(request.Id);

        if (oldIngredient == null)
        {
            return DomainErrors.Ingredients.IngredientNotFound(request.Id);
        }

        oldIngredient.Name = request.Name;

        if (oldIngredient.Stock != request.Stock)
        {
            await _ingredientLogRepository.AddAsync(
                new IngredientLog()
                {
                    IngredientId = oldIngredient.Id,
                    Date = DateTime.Now,
                    Quantity =
                        Convert.ToInt32(request.Stock) - Convert.ToInt32(oldIngredient.Stock),
                    Status =
                        request.Stock > oldIngredient.Stock
                            ? IngredientStatus.Deposit
                            : IngredientStatus.Used,
                }
            );
        }

        oldIngredient.Stock = request.Stock;

        await _ingredientRepository.UpdateAsync(oldIngredient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateIngredientCommandResponse(
            oldIngredient.Id,
            oldIngredient.Name,
            oldIngredient.Stock
        );
    }
}
