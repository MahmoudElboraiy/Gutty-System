using Domain.Models.Identity;

namespace Domain.Models.Entities;

public class ReferralCode : BaseEntity<int>
{
    public string Code { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public decimal DiscountPercentage { get; set; }
    public List<Subscription> Subscriptions { get; set; }
}
