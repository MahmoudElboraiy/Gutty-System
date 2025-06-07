using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Models.Entities;

public class Item : BaseEntity<Guid>
{
    [MaxLength(255)]
    public required string Name { get; set; }
    public required string NameAr { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public required decimal Weight { get; set; }
    public required decimal Calories { get; set; }
    public required decimal Proteins { get; set; } = 0;
    public required decimal Carbs { get; set; } = 0;
    public required decimal Fats { get; set; } = 0;
    public required decimal Fibers { get; set; } = 0;
    public required decimal BasePrice { get; set; }
    public required ItemType Type { get; set; }
    public required ICollection<ItemIngredient> Ingredients { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public decimal WeightToPriceRatio { get; set; }
}
