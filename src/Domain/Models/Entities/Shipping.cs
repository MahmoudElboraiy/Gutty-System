using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities;

public class ShippingAddress
{
    public int Id { get; set; }

    [MaxLength(50)]
    public required string City { get; set; }
    public required decimal Cost { get; set; }
    public required List<uint> Periods { get; set; }
}
