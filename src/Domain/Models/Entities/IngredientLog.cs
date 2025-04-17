using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Models.Entities;

public class IngredientLog
{
    public int Id { get; set; }
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
    public DateTime Date { get; set; }
    public int Quantity { get; set; }
    public IngredientStatus Status { get; set; }
}
