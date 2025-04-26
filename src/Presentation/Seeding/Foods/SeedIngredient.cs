using Domain.Models.Entities;
using Domain.Models.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Seeding.Foods
{
    public static class SeedIngredient
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Ingredient>()
                .HasData(
                    new Ingredient
                    {
                        Name = "Flour",
                        StockQuantity = 100.0m,
                    },
                    new Ingredient
                    {
                        Name = "Sugar",
                        StockQuantity = 50.0m,
                    },
                    new Ingredient
                    {
                        Name = "Butter",
                        StockQuantity = 30.0m,
                    }
                );
        }
    }
}
