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
    public ICollection<Meal> Meals { get; set; } = new HashSet<Meal>();
    public bool IsPreDefined { get; set; }
    public string? CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
    public uint MaxSeaFood { get; set; }
    public uint MaxMeat { get; set; }
    public uint MaxTwagen { get; set; }
    public uint MaxChicken { get; set; }
    public uint MaxPizza { get; set; }
    public uint MaxHighCarb { get; set; }
    public decimal TotalPrice { get; set; }
}
