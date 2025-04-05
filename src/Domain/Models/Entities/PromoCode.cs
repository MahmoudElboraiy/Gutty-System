using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities;

public class PromoCode
{
    public int Id { get; set; }

    [MaxLength(25)]
    public required string Code { get; set; }
    public decimal Discount { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
