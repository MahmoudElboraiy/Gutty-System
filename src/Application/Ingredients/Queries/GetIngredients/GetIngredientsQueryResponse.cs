using Domain.Models.Entities;

namespace Application.Ingredients.Queries.GetIngredients;

public record GetIngredientsQueryResponse(
    List<Ingredient> Ingredients
);