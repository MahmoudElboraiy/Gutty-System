

using Domain.Enums;

namespace Domain.Models.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public MealType MealType { get; set; }
    public ICollection<Subcategory> Subcategories { get; set; }
}
