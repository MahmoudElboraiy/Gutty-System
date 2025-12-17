
using Domain.Enums;

namespace Domain.Models.Entities;

public class PromoCode
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }
    public string? OwnerUserId { get; set; }
    public ICollection<PromoCodeUsage> Usages { get; set; } = new List<PromoCodeUsage>();
}
