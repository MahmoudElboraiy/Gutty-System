using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities;

public class Plan
{
    public int Id { get; set; }

    [MaxLength(255)]
    public required string Name { get; set; }

    [MaxLength(1000)]
    public required string Description { get; set; }
    public decimal PriceMonthly { get; set; }
    public decimal PriceWeekly { get; set; }
    public decimal PriceDaily { get; set; }
}
