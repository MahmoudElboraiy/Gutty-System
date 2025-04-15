using ErrorOr;
using MediatR;

namespace Application.Items.Queries.GetItem;

public record GetItemQuery(
    Guid Id
) : IRequest<ErrorOr<GetItemQueryResponse>>;

public record GetItemQueryResponse(
    Guid Id,
    string Name,
    string Description,
    decimal WeightRaw,
    decimal Weight,
    decimal Calories,
    decimal Fats,
    decimal Carbohydrates,
    decimal Proteins,
    bool IsMainItem,
    List<GetItemRecipeIngredientResponse> RecipeIngredients
);
public record GetItemRecipeIngredientResponse(
    int IngredientId,
    decimal Quantity
);