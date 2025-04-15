using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities;

public class Item
{
    public Guid Id { get; set; }

    [MaxLength(255)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public required string Description { get; set; }
    public decimal? WeightRaw { get; set; } = null;
    public decimal? Weight { get; set; } = null;
    public decimal? Calories { get; set; } = null;
    public required decimal Proteins { get; set; } = 0;
    public required decimal Carbohydrates { get; set; } = 0;
    public required decimal Fats { get; set; } = 0;
    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
    public List<Meal> Meals { get; set; } = [];
    public bool IsMainItem { get; set; }
}
