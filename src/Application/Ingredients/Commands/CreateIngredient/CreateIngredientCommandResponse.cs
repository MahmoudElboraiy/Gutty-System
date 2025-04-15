namespace Application.Ingredients.Commands.CreateIngredient;

public record CreateIngredientCommandResponse(
    int Id,
    string Name,
    decimal Stock
    );