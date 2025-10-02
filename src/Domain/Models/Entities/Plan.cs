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
    public uint LMealsPerDay { get; set; }
    public uint BDMealsPerDay { get; set; }
    public decimal BreakfastPrice { get; set; }
    public decimal DinnerPrice { get; set; }

    public uint CarbGrams { get; set; }
    public uint MaxCarbGrams { get; set; }
    public ICollection<PlanCategory> LunchCategories { get; set; } = new List<PlanCategory>();

    public decimal GetTotalPrice()
    {
        return BreakfastPrice + DinnerPrice + LunchCategories.Sum(c => c.GetCategoryPrice());
    }
}
    //[MaxLength(255)]
    //public required string Name { get; set; }

    //[MaxLength(1000)]
    //public required string Description { get; set; }
    //public uint DurationInDays { get; set; }
    //public uint LunchMealsPerDay { get; set; }
    //public uint DinnerMealsPerDay { get; set; }
    //public uint BreakfastMealsPerDay { get; set; }
    //public uint MaxSeaFood { get; set; } 
    //public uint MaxMeat { get; set; }
    //public uint MaxTwagen { get; set; }
    //public uint MaxChicken { get; set; }
    //public uint MaxPizza { get; set; }
    //public uint MaxHighCarb { get; set; }
    //public decimal Price { get; set; }