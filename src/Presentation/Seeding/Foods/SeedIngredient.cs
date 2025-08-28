using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Seeding.Foods
{
    public static class SeedIngredient
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            //if (await context.Ingredients.AnyAsync())
            //    return;


         //   await context.Ingredients.AddRangeAsync(ingredients);
           // await context.SaveChangesAsync();
        }
    }
}
