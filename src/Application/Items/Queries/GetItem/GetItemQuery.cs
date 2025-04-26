using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Items.Queries.GetItem;

public record GetItemQuery(Guid Id) : IRequest<ErrorOr<GetItemQueryResponse>>;

public record GetItemQueryResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Weight,
    decimal Calories,
    decimal Fats,
    decimal Carbohydrates,
    decimal Proteins,
    decimal Fibers,
    ItemType Type, 
    List<string> ImageUrls,
    List<GetItemRecipeIngredientResponse> RecipeIngredients,
    List<GetItemExtraItemOptionsResponse> ExtraItemOptions
);

public record GetItemRecipeIngredientResponse(int IngredientId, decimal Quantity);

public record GetItemExtraItemOptionsResponse(decimal Price, decimal Weight);
