using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Items.Commands.UpdateItem;

public record UpdateItemCommand(
    Guid Id,
    string Name,
    string NameAr,
    string Description,
    string DescriptionAr,
    decimal WeightRaw,
    decimal Weight,
    decimal Calories,
    decimal Fats,
    decimal Carbohydrates,
    decimal Proteins,
    decimal Fibers,
    decimal BasePrice,
    ItemType Type,
    List<string> ImageUrls,
    List<UpdateItemRecipeIngredient> RecipeIngredients,
    decimal WeightToPriceRatio
) : IRequest<ErrorOr<UpdateItemCommandResponse>>;

public record UpdateItemRecipeIngredient(int IngredientId, decimal Quantity);

public record UpdateItemExtraItemOptions(decimal Price, decimal Weight);

public record UpdateItemCommandResponse(Guid Id);
