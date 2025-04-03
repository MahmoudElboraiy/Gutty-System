using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Identity;

public class User : IdentityUser
{
    [MaxLength(255)]
    public required string FirstName { get; set; }

    [MaxLength(255)]
    public string SecondName { get; set; } = string.Empty;

    [MaxLength(255)]
    public required string LastName { get; set; }

    [MaxLength(255)]
    public required string MainAddress { get; set; }

    [MaxLength(255)]
    public string SecondaryAddress { get; set; } = string.Empty;

    [MaxLength(11)]
    [MinLength(11)]
    public string SecondPhoneNumber { get; set; } = string.Empty;
    public bool IsTestUser { get; set; } = false;

    // Override IdentityUser
    public override string? Email { get; set; } = null;
    public override bool EmailConfirmed { get; set; } = false;
}
