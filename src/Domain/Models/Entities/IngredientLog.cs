using Domain.Enums;

namespace Domain.Models.Entities;

public class IngredientLog
{
    public int Id { get; set; }
    public int IngredientId { get; set; }
    public DateTime Date { get; set; }
    public int Quantity { get; set; }
    public IngredientStatus Status { get; set; }
}
