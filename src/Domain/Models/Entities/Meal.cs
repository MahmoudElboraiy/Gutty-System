using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Models.Entities;

public class Meal : BaseEntity<Guid>
{
    public Plan? Plan { get; set; }
    public Guid PlanId { get; set; }

    [ForeignKey("Item")]
    public required Guid ItemId { get; set; }
    public required Item Item { get; set; }
    public int? ExtraItemOptionId { get; set; }
    public ExtraItemOption? ExtraItemOption { get; set; }
    public MealType MealType { get; set; }
    public uint Quantity { get; set; }
}
