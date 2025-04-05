namespace Domain.Models.Entities;

public class IngredientStock
{
    public int Id { get; set; }
    public int IngredientId { get; set; }
    public required Ingredient Ingredient { get; set; }
    public int Stock { get; set; }
}
