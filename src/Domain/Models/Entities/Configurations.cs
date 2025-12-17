

namespace Domain.Models.Entities;

public class Configurations
{
    public int Id { get; set; }
    public int DailyCapacity { get; set; }
    public int MinimumDaysToOrder { get; set; }
    public int MaximumDaysToOrder { get; set; }
 }
