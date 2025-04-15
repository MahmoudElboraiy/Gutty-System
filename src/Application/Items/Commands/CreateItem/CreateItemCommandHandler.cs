using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Items.Commands.CreateItem;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ErrorOr<CreateItemCommandResponse>>
{
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIngredientRepository _ingredientRepository;

    public CreateItemCommandHandler(IIngredientRepository ingredientRepository, IItemRepository itemRepository, IUnitOfWork unitOfWork)
    {
        _ingredientRepository = ingredientRepository;
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CreateItemCommandResponse>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var item = new Item()
        {
            Name = request.Name,
            Description = request.Description,
            Proteins = request.Proteins,
            Fats = request.Fats,
            Weight = request.Weight,
            WeightRaw = request.WeightRaw,
            Calories = request.Calories,
            Carbohydrates = request.Carbohydrates,
            IsMainItem = request.IsMainItem
        };
        
        var recipeIngredients = new List<RecipeIngredient>();
        foreach (var recipeIngredient in request.RecipeIngredients)
        {
            var ingredient = await _ingredientRepository.GetAsync(recipeIngredient.IngredientId);
            if (ingredient == null)
            {
                return DomainErrors.Ingredients.IngredientNotFound(recipeIngredient.IngredientId);
            }

            recipeIngredients.Add(new RecipeIngredient()
            {
                IngredientId = recipeIngredient.IngredientId,
                Quantity = recipeIngredient.Quantity
            });
        }

        item.RecipeIngredients = recipeIngredients;

        await _itemRepository.CreateMainItemAsync(item);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new CreateItemCommandResponse(item.Id);
    }
}