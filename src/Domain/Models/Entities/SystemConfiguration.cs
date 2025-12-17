
namespace Domain.Models.Entities;

public class SystemConfiguration
{
    public int Id { get; set; }
    public int? DailyCapacity { get; set; } 
    public int? MinimumDaysToOrder { get; set; }

}
