using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;

public class SubscriptionCategory : BaseEntity<Guid>
{
    [ForeignKey(nameof(SubscriptionId))]
    public Guid SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }

    public int? SubCategoryId { get; set; }

    public uint NumberOfMeals { get; set; }
    public uint NumberOfMealsLeft { get; set; }
    public uint ProteinGrams { get; set; }
    public decimal PricePerGram { get; set; }
    public bool AllowProteinChange { get; set; }
    public uint MaxProteinGrams { get; set; }

    public decimal GetCategoryPrice()
    {
        return NumberOfMeals * ProteinGrams * PricePerGram;
    }
}
