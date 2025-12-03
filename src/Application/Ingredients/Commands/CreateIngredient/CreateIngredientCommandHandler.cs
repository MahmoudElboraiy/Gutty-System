

using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.CreateIngredient;

public class CreateIngredientCommandHandler :
    IRequestHandler<CreateIngredientCommand, ErrorOr<CreateIngredientCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateIngredientCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<CreateIngredientCommandResponse>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredient = new Ingredient
        {
            Name = request.Name,
            CaloriesPer100g = request.CaloriesPer100g,
            ProteinPer100g = request.ProteinPer100g,
            CarbsPer100g = request.CarbsPer100g,
            FatsPer100g = request.FatsPer100g
        };
        await _unitOfWork.Ingredients.AddAsync(ingredient);
        await _unitOfWork.CompleteAsync();
        var response = new CreateIngredientCommandResponse(ingredient.Id);
        return response;
    }
}
