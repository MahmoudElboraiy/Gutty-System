using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;

public class UserPrefernce
{
    //aa
    public int Id { get; set; }
    public string UserId { get; set; }

    [MaxLength(1000)]
    public string Notes { get; set; } = string.Empty;

}
