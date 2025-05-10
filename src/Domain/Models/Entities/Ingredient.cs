using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities;

public class Ingredient : BaseEntity<int>
{
    [MaxLength(255)]
    public required string Name { get; set; }

    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;
    public decimal StockQuantity { get; set; }
    public ICollection<IngredientChange>? Changes { get; set; }
}
