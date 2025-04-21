using System.ComponentModel.DataAnnotations;
using Domain.Models.Identity;

namespace Domain.Models.Entities;

public class PromoCode
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string UserId { get; set; } = string.Empty;

    [MaxLength(25)]
    public required string Code { get; set; }
    public decimal Discount { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<User> UsersConsumed { get; set; } = new List<User>();
}
