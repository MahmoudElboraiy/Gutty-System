using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Models.Entities;

public class Meal
{
    public Guid Id { get; set; }

    [MaxLength(255)]
    public required string Name { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
    public MealType MealType { get; set; }
    public decimal? Weight { get; set; } = null;
    public decimal? Calories { get; set; } = null;
    public required decimal Proteins { get; set; } = 0;
    public required decimal Carbohydrates { get; set; } = 0;
    public required decimal Fats { get; set; } = 0;
    public required List<Item> Item { get; set; }
}
