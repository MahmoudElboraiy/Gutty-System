using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Models.Entities;

public class IngredientChange : BaseEntity<int>
{
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal OldValue { get; set; }
    public decimal NewValue { get; set; }
}
