using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;

public class Subscription
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public Guid PlanId { get; set; }
    public int DaysLeft { get; set; }
    public required Plan Plan { get; set; }
    // TODO: Add other Properties
    // TODO: Handle Packages
}
