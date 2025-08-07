using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Identity;

namespace Domain.Models.Entities;

public class Plan : BaseEntity<Guid>
{
    [MaxLength(255)]
    public required string Name { get; set; }

    [MaxLength(1000)]
    public required string Description { get; set; }
    public uint DurationInDays { get; set; }
    public uint LunchMealsPerDay { get; set; }
    public uint DinnerMealsPerDay { get; set; }
    public uint BreakfastMealsPerDay { get; set; }
    public uint MaxSeaFood { get; set; } 
    public uint MaxMeat { get; set; }
    public uint MaxTwagen { get; set; }
    public uint MaxChicken { get; set; }
    public uint MaxPizza { get; set; }
    public uint MaxHighCarb { get; set; }
    public decimal Price { get; set; }
}
