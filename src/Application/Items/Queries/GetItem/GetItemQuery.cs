using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Items.Queries.GetItem;

public record GetItemQuery(Guid Id) : IRequest<ErrorOr<GetItemQueryResponse>>;

public record GetItemQueryResponse(
    Guid Id,
    string Name,
    string NameAr,
    string Description,
    string DescriptionAr,
    decimal Weight,
    decimal Calories,
    decimal Fats,
    decimal Carbs,
    decimal Proteins,
    decimal Fibers,
    ItemType Type,
    List<string> ImageUrls,
    List<GetItemRecipeIngredientResponse> RecipeIngredients,
    decimal WeightToPriceRatio
);

public record GetItemRecipeIngredientResponse(int IngredientId, string Name, string NameAr, decimal Quantity, bool IsOptional);

