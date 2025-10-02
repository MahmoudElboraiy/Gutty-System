
using Domain.Enums;

namespace Domain.Models.Entities;

public class Meal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }

    public decimal? FixedCalories { get; set; }
    public decimal? FixedProtein { get; set; }
    public decimal? FixedCarbs { get; set; }
    public decimal? FixedFats { get; set; }
    public MealType? MealType { get; set; }
    public bool AcceptCarb { get; set; }
    // Subcategory
    public int SubcategoryId { get; set; }
    public Subcategory Subcategory { get; set; }

    // Ingredient (the one that changes by grams)
    public int? IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }

    // Default quantity (grams) for this meal
    public decimal? DefaultQuantityGrams {  get; set; }
}
