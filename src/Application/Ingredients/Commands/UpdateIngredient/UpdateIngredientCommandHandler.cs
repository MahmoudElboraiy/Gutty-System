
using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.UpdateIngredient;

public class UpdateIngredientCommandHandler :
    IRequestHandler<UpdateIngredientCommand, ErrorOr<UpdateIngredientCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    public UpdateIngredientCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<UpdateIngredientCommandResponse>> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(request.Id);
        if (ingredient is null)
        {
            return Error.NotFound(description: $"Ingredient with id {request.Id} not found.");
        }
        ingredient.Name = request.Name;
        ingredient.CaloriesPer100g = request.CaloriesPer100g;
        ingredient.ProteinPer100g = request.ProteinPer100g;
        ingredient.CarbsPer100g = request.CarbsPer100g;
        ingredient.FatsPer100g = request.FatsPer100g;
        _unitOfWork.Ingredients.Update(ingredient);
        await _unitOfWork.CompleteAsync();
        _cacheService.IncrementVersion(CacheKeys.IngredientsVersion);
        _cacheService.IncrementVersion(CacheKeys.MealsVersion);
        var response = new UpdateIngredientCommandResponse(ingredient.Id);
        return response;
    }
}
