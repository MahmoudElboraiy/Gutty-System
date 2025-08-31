using Domain.Enums;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.DErrors.DomainErrors;

namespace Presentation.Seeding.Foods;

public static class SeedMeals
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
       if (await context.Meals.AnyAsync())
            return;

        var meals = new List<Meal>
        {
           new Meal
           {
                Name = "Grilled curry breasts",
                ImageUrl = "https://example.com/images/grilled-chicken-breast.jpg",
                Description = "A healthy grilled chicken breast meal.",
                SubcategoryId = 1,
                IngredientId = 1,
                DefaultQuantityGrams = 150,
                MealType =MealType.Protien
           },
              new Meal
              {
                 Name = "Baked chicken thighs",
                 ImageUrl = "https://example.com/images/baked-chicken-thighs.jpg",
                 Description = "Juicy baked chicken thighs with herbs.",
                 SubcategoryId = 1,
                 IngredientId = 2,
                 DefaultQuantityGrams = 150,
                 MealType=MealType.Protien
              },
              new Meal
              {
                    Name = "Okra casserole with chicken breasts",
                    ImageUrl = "https://example.com/images/steak-with-vegetables.jpg",
                    Description = "Grilled steak served with steamed vegetables.",
                    SubcategoryId = 2,
                    IngredientId = 1,
                    DefaultQuantityGrams = 150,
                    MealType = MealType.Protien
              },
              new Meal
              {
                        Name = "Mixed vegetable casserole with chicken breasts",
                        ImageUrl = "https://example.com/images/steak-with-vegetables.jpg",
                        Description = "Grilled steak served with steamed vegetables.",
                        SubcategoryId = 2,
                        IngredientId = 1,
                        DefaultQuantityGrams = 150,
                        MealType =MealType.Protien
              },
              new Meal
              {
                         Name = "Meatball",
                         ImageUrl = "https://example.com/images/steak-with-vegetables.jpg",
                         Description = "Grilled steak served with steamed vegetables.",
                         SubcategoryId = 3,
                         IngredientId = 7,
                         DefaultQuantityGrams = 150,
                         MealType =MealType.Protien
               },
               new Meal
               {
                         Name = "barbecue steak",
                         ImageUrl = "https://example.com/images/beef-stew.jpg",
                         Description = "Hearty beef stew with vegetables.",
                         SubcategoryId = 3,
                         IngredientId = 4,
                         DefaultQuantityGrams = 150,
                            MealType =MealType.Protien
               },
                new Meal
                {
                             Name = "Okra casserole with meat",
                             ImageUrl = "https://example.com/images/minced-meat-with-vegetables.jpg",
                             Description = "Savory minced meat cooked with mixed vegetables.",
                             SubcategoryId = 4,
                             IngredientId = 3,
                             DefaultQuantityGrams = 150,
                                MealType =MealType.Protien
                },
                    new Meal
                    {
                                Name = "Whitefish fillet with tahini",
                                ImageUrl = "https://example.com/images/minced-meat-with-vegetables.jpg",
                                Description = "Savory minced meat cooked with mixed vegetables.",
                                SubcategoryId = 5,
                                IngredientId = 10,
                                DefaultQuantityGrams = 150,
                                MealType =MealType.Protien
                    },
                    new Meal
                    {
                                Name = "Shrimp Fajita",
                                ImageUrl = "https://example.com/images/grilled-shrimp.jpg",
                                Description = "Delicious grilled shrimp with garlic and herbs.",
                                SubcategoryId = 5,
                                IngredientId = 11,
                                DefaultQuantityGrams = 150,
                                MealType =MealType.Protien
                    },
                    new Meal
                    {
                                Name = "Whole Chicken Ranch Pizza",
                                ImageUrl = "https://example.com/images/baked-fish-fillet.jpg",
                                Description = "Tender baked fish fillet with lemon and dill.",
                                SubcategoryId = 6,
                                FixedCalories = 300,
                                FixedProtein = 25,
                                FixedCarbs = 30,
                                FixedFats = 10,
                                MealType =MealType.Protien
                    },
                    new Meal
                    {
                                Name = "Whole Tuna Ranch Pizza",
                                ImageUrl = "https://example.com/images/hawawshi.jpg",
                                Description = "Traditional Egyptian hawawshi stuffed with spiced minced meat.",
                                SubcategoryId = 6,
                                FixedCalories =1300,
                                FixedProtein = 56,
                                FixedCarbs = 47,
                                FixedFats = 31,
                                MealType =MealType.Protien
                    },
                    new Meal
                    {
                                Name = "Hawawshi with minced meat and mozzarella",
                                ImageUrl = "https://example.com/images/rice-with-vegetables-and-chicken.jpg",
                                Description = "Flavorful rice cooked with mixed vegetables and grilled chicken.",
                                SubcategoryId = 7,
                                FixedCalories =2300,
                                FixedProtein = 58,
                                FixedCarbs = 347,
                                FixedFats = 14,
                                MealType =MealType.Protien
                    },
                    new Meal
                    {
                                Name = "Basmati rice with vegetables",
                                ImageUrl = "https://example.com/images/rice-with-vegetables-and-beef.jpg",
                                Description = "Savory rice cooked with mixed vegetables and tender beef.",
                                SubcategoryId = 8,
                                IngredientId = 13,
                                DefaultQuantityGrams = 200,
                                MealType =MealType.Carb_Rice
                    },
                    new Meal
                    {
                                Name = "macaroni with bechamel",
                                ImageUrl = "https://example.com/images/pasta-with-tomato-sauce-and-chicken.jpg",
                                Description = "Whole wheat pasta tossed in a rich tomato sauce with grilled chicken.",
                                SubcategoryId = 9,
                                IngredientId = 13,
                                DefaultQuantityGrams = 200,
                                MealType =MealType.Carb_Pasta
                    },
                    new Meal
                    {
                                Name = "Caesar Chicken Salad",
                                ImageUrl = "https://example.com/images/pasta-with-tomato-sauce-and-beef.jpg",
                                Description = "Whole wheat pasta tossed in a rich tomato sauce with tender beef.",
                                SubcategoryId = 10,
                                FixedCalories = 400,
                                FixedProtein = 35,
                                FixedCarbs = 20,
                                FixedFats = 15,
                                MealType =MealType.BreakFastAndDinner
                    },
                    new Meal
                    {
                                Name = "Chicken Cheese Roll",
                                ImageUrl = "https://example.com/images/caesar-chicken-salad.jpg",
                                Description = "Crisp romaine lettuce with grilled chicken, parmesan, and Caesar dressing.",
                                SubcategoryId = 11,
                                FixedCalories = 350,
                                FixedProtein = 30,
                                FixedCarbs = 15,
                                FixedFats = 12,
                                MealType =MealType.BreakFastAndDinner
                    },
                    new Meal
                    {
                                Name = "Mushroom egg toast",
                                ImageUrl = "https://example.com/images/turkey-avocado-roll.jpg",
                                Description = "Sliced turkey and creamy avocado wrapped in a whole wheat tortilla.",
                                SubcategoryId = 12,
                                FixedCalories = 320,
                                FixedProtein = 28,
                                FixedCarbs = 18,
                                FixedFats = 10,
                                MealType =MealType.BreakFastAndDinner
                    },
                    new Meal
                    {
                                Name = "Tuna Ranch Slides Pizza",
                                ImageUrl = "https://example.com/images/mushroom-egg-toast.jpg",
                                Description = "Classic margherita pizza with fresh tomatoes, mozzarella, and basil.",
                                SubcategoryId = 13,
                                FixedCalories = 450,
                                FixedProtein = 20,
                                FixedCarbs = 50,
                                FixedFats = 15,
                                MealType =MealType.BreakFastAndDinner
                    },
                    new Meal
                    {
                                Name = "Omelette with vegetables",
                                ImageUrl = "https://example.com/images/scrambled-eggs-with-vegetables.jpg",
                                Description = "Fluffy scrambled eggs cooked with fresh vegetables.",
                                SubcategoryId = 14,
                                FixedCalories = 250,
                                FixedProtein = 20,
                                FixedCarbs = 10,
                                FixedFats = 15,
                                MealType =MealType.BreakFastAndDinner
                    },
                    new Meal
                    {
                                Name = "Sugar-free chocolate mousse",
                                ImageUrl = "https://example.com/images/sugar-free-chocolate-mousse.jpg",
                                Description = "Decadent chocolate mousse made with sugar substitutes.",
                                SubcategoryId = 15,
                                FixedCalories = 200,
                                FixedProtein = 5,
                                FixedCarbs = 15,
                                FixedFats = 12,
                                MealType =MealType.BreakFastAndDinner
                    }

        };

        await context.Meals.AddRangeAsync(meals);
        await context.SaveChangesAsync();
    }
}
