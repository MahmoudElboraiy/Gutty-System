using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Models.Entities;

public class Subscription : BaseEntity<Guid>
{
    public string UserId { get; set; }
    public Guid PlanId { get; set; }
    public required Plan Plan { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? AppliedReferralCodeId { get; set; }
    public ReferralCode? AppliedReferralCode { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}
