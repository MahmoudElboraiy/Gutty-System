
namespace Domain.Models.Entities;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Nutritional values per 100 grams
    public decimal CaloriesPer100g { get; set; }
    public decimal ProteinPer100g { get; set; }
    public decimal CarbsPer100g { get; set; }
    public decimal FatsPer100g { get; set; }

    public ICollection<Meal> Meals { get; set; }
}
