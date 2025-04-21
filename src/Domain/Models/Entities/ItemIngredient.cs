namespace Domain.Models.Entities;

public class ItemIngredient : BaseEntity<int>
{
    public Guid MealId { get; set; }
    public LaunchMeal LaunchMeal { get; set; } = null!;
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
    public decimal Quantity { get; set; }
}