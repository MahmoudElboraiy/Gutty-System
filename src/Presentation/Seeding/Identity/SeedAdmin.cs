using Domain.Enums;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Presentation.Seeding.Identity;

public class SeedAdmin
{
    public static async Task SeedAsync(UserManager<User> userManager)
    {
//<<<<<<< HEAD
//=======
        if (await userManager.FindByEmailAsync("0wPZG@example.com") != null)
            return;

//>>>>>>> cba8637334410772053f5b81c31b23463032c794
        var admin = new User
        {
            FirstName = "Admin",
            MiddleName = "Admin",
            LastName = "Admin",
            PhoneNumber = "01000000000",
            SecondPhoneNumber = "01000000000",
            Email = "0wPZG@example.com",
            MainAddress = "Admin Address",
            UserName = "0wPZG@example.com",
            CityId = 1,
        };

        await userManager.CreateAsync(admin, "Admin123");
        await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
    }
}
