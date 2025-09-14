namespace Domain.Models.Entities;

public class ItemIngredient : BaseEntity<int>
{
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = null!;
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
    public bool IsOptional { get; set; } = false;
    public decimal Quantity { get; set; }
}
