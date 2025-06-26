using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Items.Commands.CreateItem;

public class CreateItemCommandHandler
    : IRequestHandler<CreateItemCommand, ErrorOr<CreateItemCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CreateItemCommandResponse>> Handle(
        CreateItemCommand request,
        CancellationToken cancellationToken
    )
    {
        var item = new Item()
        {
            Name = request.Name,
            NameAr = request.NameAr,
            Description = request.Description,
            DescriptionAr = request.DescriptionAr,
            Proteins = request.Proteins,
            Fats = request.Fats,
            Weight = request.Weight,
            Calories = request.Calories,
            Carbs = request.Carbohydrates,
            BasePrice = request.BasePrice,
            ImageUrls = request.ImageUrls,
            Fibers = request.Fibers,
            Type = request.ItemType,
            MenuType = request.MenuType,
            Ingredients = [],
            WeightToPriceRatio = request.WeightToPriceRatio
        };

        var recipeIngredients = new List<ItemIngredient>();
        foreach (var recipeIngredient in request.RecipeIngredients)
        {
            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(
                recipeIngredient.IngredientId
            );
            if (ingredient == null)
            {
                return DomainErrors.Ingredients.IngredientNotFound(recipeIngredient.IngredientId);
            }

            recipeIngredients.Add(
                new ItemIngredient()
                {
                    IngredientId = recipeIngredient.IngredientId,
                    Quantity = recipeIngredient.Quantity,
                }
            );
        }

        item.Ingredients = recipeIngredients;

        await _unitOfWork.Items.AddAsync(item);

        await _unitOfWork.CompleteAsync();

        return new CreateItemCommandResponse(item.Id);
    }
}
