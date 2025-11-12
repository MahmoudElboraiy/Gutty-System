using Domain.Enums;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Presentation.Seeding.Identity;

public class SeedAdmin
{
    public static async Task SeedAsync(UserManager<User> userManager)
    {

        var admin = new User
        {
            FirstName = "Ahmed",
            MiddleName = "",
            LastName = "Ashraf",
            PhoneNumber = "01124559909",
            SecondPhoneNumber = "01000000000",
            Email = "ahmedashraf21499@gmail.com",
            MainAddress = "Cairo",
            UserName = "01124559909",
        };

        await userManager.CreateAsync(admin, "Admin123*");
        await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
    }
}
