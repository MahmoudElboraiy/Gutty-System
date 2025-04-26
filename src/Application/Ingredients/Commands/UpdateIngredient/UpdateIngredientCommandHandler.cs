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

    public UpdateIngredientCommandHandler(
        IUnitOfWork unitOfWork
    )
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<UpdateIngredientCommandResponse>> Handle(
        UpdateIngredientCommand request,
        CancellationToken cancellationToken
    )
    {
        var oldIngredient = await _unitOfWork.Ingredients.GetByIdAsync(request.Id);

        if (oldIngredient == null)
        {
            return DomainErrors.Ingredients.IngredientNotFound(request.Id);
        }

        oldIngredient.Name = request.Name;

        if (oldIngredient.StockQuantity != request.Stock)
        {
            await _unitOfWork.IngredientChanges.AddAsync(
                new IngredientChange()
                {
                    IngredientId = oldIngredient.Id,
                    Quantity =
                        Convert.ToInt32(request.Stock) - Convert.ToInt32(oldIngredient.StockQuantity),
                    OldValue = oldIngredient.StockQuantity,
                    NewValue = request.Stock,
                }
            );
        }

        oldIngredient.StockQuantity = request.Stock;

        _unitOfWork.Ingredients.Update(oldIngredient);

        await _unitOfWork.CompleteAsync();

        return new UpdateIngredientCommandResponse(
            oldIngredient.Id,
            oldIngredient.Name,
            oldIngredient.StockQuantity
        );
    }
}
