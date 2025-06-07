using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Items.Commands.CreateItem;

public record CreateItemCommand(
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
    decimal BasePrice,
    decimal Fibers,
    List<string> ImageUrls,
    ItemType ItemType,
    List<CreateItemRecipeIngredient> RecipeIngredients,
    decimal WeightToPriceRatio
) : IRequest<ErrorOr<CreateItemCommandResponse>>;

public record CreateItemRecipeIngredient(int IngredientId, int Quantity);

public record CreateItemCommandResponse(Guid Id);
