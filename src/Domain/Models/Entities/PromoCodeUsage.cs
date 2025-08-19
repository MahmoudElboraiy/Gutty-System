

namespace Domain.Models.Entities;

public class PromoCodeUsage
{
    public Guid Id { get; set; }
    public Guid PromoCodeId { get; set; }
    public string UserId { get; set; }
    public DateTime UsedAt { get; set; }
  public PromoCode PromoCode { get; set; }
}
