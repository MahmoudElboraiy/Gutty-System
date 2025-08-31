using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Seeding.Foods;

public static class SeedSubCategories
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Subcategories.AnyAsync())
            return;

        var subCategories = new List<Subcategory>
        {
            new Subcategory { Name = "Chicken Menu", CategoryId = 2 }, 
            new Subcategory { Name = "Chicken Twagen", CategoryId = 2 },
            new Subcategory { Name = "Meat Menu", CategoryId = 2 },
            new Subcategory { Name = "Twagen Meat", CategoryId = 2 },
            new Subcategory { Name = "Sea Food Menu", CategoryId = 2 },
            new Subcategory { Name = "Pizaa Menu", CategoryId = 2 },
            new Subcategory { Name = "Hawawshi Menu", CategoryId = 2 },
            new Subcategory { Name = "Rice Menu", CategoryId = 3 },
            new Subcategory { Name = "Pasta Menu", CategoryId = 3 }, 
            new Subcategory { Name = "Protein Salads Menu", CategoryId = 1 },
            new Subcategory { Name = "Rolls Menu", CategoryId = 1 },
            new Subcategory { Name = "Toast Menu", CategoryId = 1 },
            new Subcategory { Name = "Pizaa Menu", CategoryId = 1 },
            new Subcategory { Name = "Eggs Menu", CategoryId = 1 },
            new Subcategory { Name = "Sugar Free Menu", CategoryId = 1 }
        };

        await context.Subcategories.AddRangeAsync(subCategories);
        await context.SaveChangesAsync();
    }
}
