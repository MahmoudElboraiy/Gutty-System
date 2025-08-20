using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Models.Entities;

//public class Subscription : BaseEntity<Guid>
//{
//    public string UserId { get; set; }
//    public Guid PlanId { get; set; }
//    public required Plan Plan { get; set; }
//    public int? AppliedReferralCodeId { get; set; }
//    public ReferralCode? AppliedReferralCode { get; set; }
//    public decimal Amount { get; set; }
//    public DateTime PaymentDate { get; set; }
//    public PaymentStatus PaymentStatus { get; set; }
//}
public class Subscription : BaseEntity<Guid>
{
    public string UserId { get; set; }
    public string PlanName { get; set; }
    public uint DurationInDays { get; set; }
    public decimal BreakfastPrice { get; set; }
    public decimal DinnerPrice { get; set; }
    public uint PastaCarbGrams { get; set; }
    public uint RiceCarbGrams { get; set; }
    public uint MaxRiceCarbGrams { get; set; }
    public uint MaxPastaCarbGrams { get; set; }
    public DateTime StartDate { get; set; }
    public Guid? PromoCodeId { get; set; }
    public PromoCode? PromoCode { get; set; }
    public bool IsActive { get; set; }
    public ICollection<SubscriptionCategory> LunchCategories { get; set; } = new List<SubscriptionCategory>();
    public decimal GetTotalPrice()
    {
        return BreakfastPrice + DinnerPrice + LunchCategories.Sum(c => c.GetCategoryPrice());
    }
}