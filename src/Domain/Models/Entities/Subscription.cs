using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Domain.Models.Identity;

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
    public User User { get; set; }
    public Guid PlanId { get; set; }
    public  Plan Plan { get; set; }
    // public string PlanName { get; set; }
    public uint DaysLeft { get; set; }
    public uint LunchMealsLeft { get; set; }
   // public decimal BreakfastPrice { get; set; }
   // public decimal DinnerPrice { get; set; }
    public uint CarbGrams { get; set; }
    public DateTime StartDate { get; set; }
    public Guid? PromoCodeId { get; set; }
    public PromoCode? PromoCode { get; set; }
    public bool IsCurrent { get; set; }
    public bool IsPaused { get; set; }
    public ICollection<SubscriptionCategory> LunchCategories { get; set; } = new List<SubscriptionCategory>();
    public decimal GetTotalPrice()
    {
        return Plan.BreakfastPrice + Plan.DinnerPrice + LunchCategories.Sum(c => c.GetCategoryPrice());
    }
}