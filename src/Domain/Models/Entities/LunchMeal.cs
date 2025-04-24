using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Models.Entities;

public class LunchMeal : BaseEntity<Guid>
{
    public Plan? Plans { get; set; }
    [ForeignKey("Item")]
    public Guid ProteinItemId { get; set; }
    public Item ProteinItem { get; set; } = null!;
    [ForeignKey("Item")]
    public Guid CarbohydrateItemId { get; set; }
    public Item CarbohydrateItem { get; set; } = null!;
    public decimal TotalWeight { get; set; }
    public decimal TotalCalories { get; set; }
    public decimal TotalFats { get; set; }
    public decimal TotalCarbs { get; set; }
    public decimal TotalProteins { get; set; }
    public decimal TotalPrice { get; set; }
    public uint Quantity { get; set; }
    public decimal TotalQuantityPrice => TotalPrice * Quantity;
}
