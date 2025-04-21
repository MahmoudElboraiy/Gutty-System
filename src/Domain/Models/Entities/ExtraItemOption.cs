namespace Domain.Models.Entities;

public class ExtraItemOption : BaseEntity<int>
{
    public Guid ItemId { get; set; }
    public required Item Item { get; set; }
    public decimal Weight { get; set; } 
    public decimal Price { get; set; } 
    public decimal Calories { get; set; }
    public decimal Proteins { get; set; } = 0;
    public decimal Carbs { get; set; } = 0;
    public decimal Fats { get; set; } = 0;
}