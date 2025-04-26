using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.CreateIngredient;

public class CreateIngredientCommandHandler
    : IRequestHandler<CreateIngredientCommand, ErrorOr<CreateIngredientCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateIngredientCommandHandler(
        IUnitOfWork unitOfWork
    )
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CreateIngredientCommandResponse>> Handle(
        CreateIngredientCommand request,
        CancellationToken cancellationToken
    )
    {
        var ingredient = new Ingredient()
        {
            Name = request.Name,
            StockQuantity = request.Stock,
            Description = request.Description
        };

        await _unitOfWork.Ingredients.AddAsync(ingredient);

        await _unitOfWork.CompleteAsync();

        if (request.Stock != 0)
        {
            await _unitOfWork.IngredientChanges.AddAsync(
                new IngredientChange
                {
                    IngredientId = ingredient.Id,
                    Quantity = request.Stock,
                    OldValue = 0,
                    NewValue = request.Stock,
                }
            );
        }

        await _unitOfWork.CompleteAsync();

        return new CreateIngredientCommandResponse(
            ingredient.Id,
            ingredient.Name,
            ingredient.StockQuantity
        );
    }
}
