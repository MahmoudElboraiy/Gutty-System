using Domain.Enums;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Seeding.Foods;

public static class SeedCategories
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;

        var categories = new List<Category>
        {
            new Category { Name = "Breakfast And Dinner" ,MealType=MealType.BreakFastAndDinner},
            new Category { Name = "Protein Lunch" ,MealType =MealType.Protien},
            new Category { Name = "Carb Lunch" , MealType =MealType.Carb}
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}
