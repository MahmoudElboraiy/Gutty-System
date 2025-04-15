using MediatR;
using ErrorOr;

namespace Application.Items.Commands.UpdateItem;

public record UpdateItemCommand(
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
    List<UpdateItemRecipeIngredient> RecipeIngredients 
) : IRequest<ErrorOr<UpdateItemCommandResponse>>;

public record UpdateItemRecipeIngredient(int IngredientId, decimal Quantity);

public record UpdateItemCommandResponse(Guid Id);