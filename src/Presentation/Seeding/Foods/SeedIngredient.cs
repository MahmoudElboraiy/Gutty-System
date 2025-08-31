using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Seeding.Foods;

public static class SeedIngredient
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Ingredients.AnyAsync())
            return;
        var ingredients = new List<Ingredient>
        {
            new Ingredient {
                //1
                Name = "Chicken Breast",
                CaloriesPer100g = 165,
                ProteinPer100g = 31,
                CarbsPer100g = 0,
                FatsPer100g = 3.6m
            },
            new Ingredient {
                //2
                Name = "Chicken thighs",
                CaloriesPer100g = 112,
                ProteinPer100g = 26m,
                CarbsPer100g = 23,
                FatsPer100g = 0.9m
            },
            new Ingredient {
                //3
                Name = "Meat pieces",
                CaloriesPer100g = 55,
                ProteinPer100g = 37m,
                CarbsPer100g = 11.1m,
                FatsPer100g = 0.6m
            },
            new Ingredient {
                //4
                Name = "Steak pieces",
                CaloriesPer100g = 206,
                ProteinPer100g = 22,
                CarbsPer100g = 0,
                FatsPer100g = 12
            },
            new Ingredient {
                //5
                Name = "Minced meat",
                CaloriesPer100g = 120,
                ProteinPer100g = 41m,
                CarbsPer100g = 21.3m,
                FatsPer100g = 1.9m
            },
            new Ingredient {
                //6
                Name = "Minced meatball",
                CaloriesPer100g = 23,
                ProteinPer100g = 29m,
                CarbsPer100g = 3.6m,
                FatsPer100g = 0.4m
            },
            new Ingredient {
                //7
                Name = "Meatballs",
                CaloriesPer100g = 155,
                ProteinPer100g = 33,
                CarbsPer100g = 1.1m,
                FatsPer100g = 11
            },
            new Ingredient {
                //8
                Name = "Minced meat kofta",
                CaloriesPer100g = 86,
                ProteinPer100g = 36m,
                CarbsPer100g = 20.1m,
                FatsPer100g = 0.1m
            },
            new Ingredient {
                //9
                Name = "Minced meat burger",
                CaloriesPer100g = 59,
                ProteinPer100g = 30,
                CarbsPer100g = 3.6m,
                FatsPer100g = 0.4m
            },
            new Ingredient {
                //10
                Name = "Pan-white fish fillet",
                CaloriesPer100g = 206,
                ProteinPer100g = 22,
                CarbsPer100g = 0,
                FatsPer100g = 12
            },
            new Ingredient {
                //11
                Name = "Medium shrimp",
                CaloriesPer100g = 206,
                ProteinPer100g = 22,
                CarbsPer100g = 0,
                FatsPer100g = 12
            },
            new Ingredient {
                //12
                Name = "Rice",
                CaloriesPer100g = 206,
                ProteinPer100g = 2,
                CarbsPer100g = 400,
                FatsPer100g = 12
            },
            new Ingredient {
                //13
                Name = "Pasta",
                CaloriesPer100g = 130,
                ProteinPer100g = 2.7m,
                CarbsPer100g = 500,
                FatsPer100g = 0.3m
            },

        };

        await context.Ingredients.AddRangeAsync(ingredients);
        await context.SaveChangesAsync();
    }
}
