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
}
