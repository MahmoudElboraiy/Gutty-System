using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Presentation.Seeding.Identity;

public class SeedRoles
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
    }
}
