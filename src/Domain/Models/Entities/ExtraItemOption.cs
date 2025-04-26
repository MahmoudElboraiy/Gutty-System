namespace Domain.Models.Entities;

public class ExtraItemOption : BaseEntity<int>
{
    public Guid ItemId { get; set; }
    public required Item Item { get; set; }
    public decimal Weight { get; set; } 
    public decimal Price { get; set; } 
}