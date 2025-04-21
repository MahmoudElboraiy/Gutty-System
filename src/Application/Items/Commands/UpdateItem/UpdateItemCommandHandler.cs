using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Items.Commands.UpdateItem;

public class UpdateItemCommandHandler
    : IRequestHandler<UpdateItemCommand, ErrorOr<UpdateItemCommandResponse>>
{
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IRecipeIngredientRepository _recipeIngredientRepository;

    public UpdateItemCommandHandler(
        IRecipeIngredientRepository recipeIngredientRepository,
        IIngredientRepository ingredientRepository,
        IUnitOfWork unitOfWork,
        IItemRepository itemRepository
    )
    {
        _recipeIngredientRepository = recipeIngredientRepository;
        _ingredientRepository = ingredientRepository;
        _unitOfWork = unitOfWork;
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<UpdateItemCommandResponse>> Handle(
        UpdateItemCommand request,
        CancellationToken cancellationToken
    )
    {
        var item = await _itemRepository.GetItemByIdAsync(request.Id);
        if (item == null)
        {
            return DomainErrors.Items.ItemNotFound(request.Id);
        }

        item.Name = request.Name;
        item.Description = request.Description;
        item.Proteins = request.Proteins;
        item.Fats = request.Fats;
        item.Weight = request.Weight;
        item.WeightRaw = request.WeightRaw;
        item.Calories = request.Calories;
        item.Carbohydrates = request.Carbohydrates;
        item.IsMainItem = request.IsMainItem;

        var toRemove = item.RecipeIngredients.ToList();
        foreach (var recipeIngredient in toRemove)
        {
            await _recipeIngredientRepository.DeleteAsync(recipeIngredient);
        }

        var toAdd = new List<RecipeIngredient>();
        foreach (var recipeIngredient in request.RecipeIngredients)
        {
            var ingredient = await _ingredientRepository.GetAsync(recipeIngredient.IngredientId);
            if (ingredient == null)
            {
                return DomainErrors.Ingredients.IngredientNotFound(recipeIngredient.IngredientId);
            }

            toAdd.Add(
                new RecipeIngredient()
                {
                    IngredientId = recipeIngredient.IngredientId,
                    Quantity = recipeIngredient.Quantity,
                }
            );
        }

        item.RecipeIngredients = toAdd;

        await _itemRepository.UpdateMainItemAsync(item);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateItemCommandResponse(item.Id);
    }
}
