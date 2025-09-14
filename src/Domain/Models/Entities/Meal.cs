using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Models.Entities;

public class Meal : BaseEntity<Guid>
{
    [ForeignKey("Item")]
    public required Guid ItemId { get; set; }
    public required Item Item { get; set; }
    public decimal Weight { get; set; }
    public decimal Price { get; set; }
    public MealType MealType { get; set; }
    public uint Quantity { get; set; }
    // ============================================
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
    public int MealCategoryId { get; set; }
    public int PreparationTime { get; set; }
    public int Calories { get; set; }

    //public virtual MealCategory MealCategory { get; set; }
    //public virtual ICollection<MealFood> Ingredients { get; set; } = new List<MealFood>();
}
