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
            modelBuilder.Entity<Ingredient>().HasData(
              new Ingredient
              {
                  Id = 1,
                  Name = "Flour",
                  AmountNeeded = 5.0m,
                  Stock = 100.0m
              },
              new Ingredient
              {
                  Id = 2,
                  Name = "Sugar",
                  AmountNeeded = 2.0m,
                  Stock = 50.0m
              },
              new Ingredient
              {
                  Id = 3,
                  Name = "Butter",
                  AmountNeeded = 1.0m,
                  Stock = 30.0m
              }
          );
        }
    }
}
