using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Items.Commands.UpdateItem;

public class UpdateItemCommandHandler
    : IRequestHandler<UpdateItemCommand, ErrorOr<UpdateItemCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<UpdateItemCommandResponse>> Handle(
        UpdateItemCommand request,
        CancellationToken cancellationToken
    )
    {
        var item = await _unitOfWork.Items.GetByIdAsync(request.Id);
        if (item == null)
        {
            return DomainErrors.Items.ItemNotFound(request.Id);
        }

        item.Name = request.Name;
        item.Description = request.Description;
        item.Proteins = request.Proteins;
        item.Fats = request.Fats;
        item.Weight = request.Weight;
        item.Calories = request.Calories;
        item.Carbs = request.Carbohydrates;
        item.Fibers = request.Fibers;
        item.ImageUrls = request.ImageUrls;
        item.Type = request.Type;

        var toRemove = item.Ingredients.ToList();
        foreach (var recipeIngredient in toRemove)
        {
            _unitOfWork.ItemIngredients.Remove(recipeIngredient);
        }

        var toAdd = new List<ItemIngredient>();
        foreach (var recipeIngredient in request.RecipeIngredients)
        {
            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(
                recipeIngredient.IngredientId
            );
            if (ingredient == null)
            {
                return DomainErrors.Ingredients.IngredientNotFound(recipeIngredient.IngredientId);
            }

            toAdd.Add(
                new ItemIngredient()
                {
                    IngredientId = recipeIngredient.IngredientId,
                    Quantity = recipeIngredient.Quantity,
                }
            );
        }

        if (request.ExtraItemOptions != null)
        {
            var toRemoveExtraItemOptions = item.ExtraItemOptions;

            if (toRemoveExtraItemOptions != null)
            {
                foreach (var extraItemOption in toRemoveExtraItemOptions)
                {
                    _unitOfWork.ExtraItemOptions.Remove(extraItemOption);
                }
            }

            foreach (var extraItemOption in request.ExtraItemOptions)
            {
                var extraItemOptionEntity = new ExtraItemOption()
                {
                    Item = item,
                    ItemId = item.Id,
                    Weight = extraItemOption.Weight,
                    Price = extraItemOption.Price,
                };
                await _unitOfWork.ExtraItemOptions.AddAsync(extraItemOptionEntity);
            }
        }

        item.Ingredients = toAdd;

        _unitOfWork.Items.Update(item);

        await _unitOfWork.CompleteAsync();

        return new UpdateItemCommandResponse(item.Id);
    }
}
