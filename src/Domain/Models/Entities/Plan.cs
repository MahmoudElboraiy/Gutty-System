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
    public ICollection<BreakFastOrDinnerMeal>  BreakFastOrDinnerMeals { get; set; } = new HashSet<BreakFastOrDinnerMeal>();
    public ICollection<LunchMeal> LaunchMeals { get; set; } = new HashSet<LunchMeal>();
    public bool IsPreDefined { get; set; }
    public string? CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
    public decimal TotalPrice { get; set; }
}
