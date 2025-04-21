namespace Domain.Models.Entities;

public class BreakFastOrDinnerMeal
{
    public Plan? Plan { get; set; }
    public Guid PlanId { get; set; }
    public Item? Item { get; set; }
    public Guid ItemId { get; set; }
    public decimal TotalWeight { get; set; }
    public decimal TotalCalories { get; set; }
    public decimal TotalFats { get; set; }
    public decimal TotalCarbs { get; set; }
    public decimal TotalProteins { get; set; }
    public decimal TotalPrice { get; set; }
    public uint Quantity { get; set; }
    public decimal TotalQuantityPrice => TotalPrice * Quantity;
}
