using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities;

public class Ingredient
{
    public int Id { get; set; }

    [MaxLength(255)]
    public required string Name { get; set; }
    public decimal AmountNeeded { get; set; }
    public decimal Stock { get; set; }
}
