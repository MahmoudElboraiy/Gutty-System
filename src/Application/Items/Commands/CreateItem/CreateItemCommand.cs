using ErrorOr;
using MediatR;

namespace Application.Items.Commands.CreateItem;

public record CreateItemCommand(
    string Name,
    string Description,
    decimal WeightRaw,
    decimal Weight,
    decimal Calories,
    decimal Fats,
    decimal Carbohydrates,
    decimal Proteins,
    bool IsMainItem,
    List<CreateItemRecipeIngredient> RecipeIngredients
) : IRequest<ErrorOr<CreateItemCommandResponse>>;

public record CreateItemRecipeIngredient(int IngredientId, int Quantity);

public record CreateItemCommandResponse(Guid Id);
