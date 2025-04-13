using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities;

public class UserPrefernce
{
    //aa
    public int Id { get; set; }
    public int UserId { get; set; }

    [MaxLength(1000)]
    public string Notes { get; set; } = string.Empty;

    public List<Meal> UnlovedMeals { get; set; } = [];
}
