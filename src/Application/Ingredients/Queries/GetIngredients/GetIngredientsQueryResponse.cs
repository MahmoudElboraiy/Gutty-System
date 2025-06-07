using Domain.Models.Entities;

namespace Application.Ingredients.Queries.GetIngredients;

public record GetIngredientsQueryResponse(List<GetIngredientsQueryResponseItem> Ingredients);

public record GetIngredientsQueryResponseItem(int Id, string Name, string NameAr, decimal Stock);
