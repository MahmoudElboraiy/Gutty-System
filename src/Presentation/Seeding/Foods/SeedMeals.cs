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
                ImageUrl = "https://ichef.bbci.co.uk/food/ic/food_16x9_1600/recipes/air_fryer_roast_chicken_27390_16x9.jpg",
                Description = "A healthy grilled chicken breast meal.",
                SubcategoryId = 1,
                IngredientId = 1,
                DefaultQuantityGrams = 150,
                MealType =MealType.Protien,
                AcceptCarb = true
           },
              new Meal
              {
                 Name = "Baked chicken thighs",
                 ImageUrl = "https://www.onceuponachef.com/images/2011/01/roast-chicken-1.jpg",
                 Description = "Juicy baked chicken thighs with herbs.",
                 SubcategoryId = 1,
                 IngredientId = 2,
                 DefaultQuantityGrams = 150,
                 MealType=MealType.Protien,
                 AcceptCarb = true
              },
              new Meal
              {
                    Name = "Okra casserole with chicken breasts",
                    ImageUrl = "https://www.alisoneroman.com/content/images/size/w1200/format/avif/images-squarespace-cdn-com/content/v1/541b1515e4b0a990b33a796e/41af167f-941a-4d42-ad21-32a5db11a1a9/grilled_chicken_with_spicy_lime.jpg",
                    Description = "Grilled steak served with steamed vegetables.",
                    SubcategoryId = 2,
                    IngredientId = 1,
                    DefaultQuantityGrams = 150,
                    MealType = MealType.Protien,
                    AcceptCarb = false
              },
              new Meal
              {
                        Name = "Mixed vegetable casserole with chicken breasts",
                        ImageUrl = "https://www.alisoneroman.com/content/images/size/w1200/format/avif/images-squarespace-cdn-com/content/v1/541b1515e4b0a990b33a796e/1634744280316-0C8HLYGBQ5D2F7PSHRTT/2018_09_23_chicken_0383_2.jpg",
                        Description = "Grilled steak served with steamed vegetables.",
                        SubcategoryId = 2,
                        IngredientId = 1,
                        DefaultQuantityGrams = 150,
                        MealType =MealType.Protien,
                        AcceptCarb = false
              },
              new Meal
              {
                         Name = "Meatball",
                         ImageUrl = "https://media.istockphoto.com/id/1545218890/photo/roast-pork-loin-mushrooms-in-sauce-and-fresh-vegetables-on-wooden-table.webp?s=2048x2048&w=is&k=20&c=xcAI2pptwsdSF0pd873nYwreqbandLre3HQmhzTO9h4=",
                         Description = "Grilled steak served with steamed vegetables.",
                         SubcategoryId = 3,
                         IngredientId = 7,
                         DefaultQuantityGrams = 150,
                         MealType =MealType.Protien,
                         AcceptCarb = true
               },
               new Meal
               {
                         Name = "barbecue steak",
                         ImageUrl = "https://www.alisoneroman.com/content/images/size/w1200/format/avif/images-squarespace-cdn-com/content/v1/541b1515e4b0a990b33a796e/1634744280316-0C8HLYGBQ5D2F7PSHRTT/2018_09_23_chicken_0383_2.jpg",
                         Description = "Hearty beef stew with vegetables.",
                         SubcategoryId = 3,
                         IngredientId = 4,
                         DefaultQuantityGrams = 150,
                         MealType =MealType.Protien,
                         AcceptCarb = true
               },
                new Meal
                {
                             Name = "Okra casserole with meat",
                             ImageUrl = "https://www.alisoneroman.com/content/images/size/w1200/format/avif/images-squarespace-cdn-com/content/v1/541b1515e4b0a990b33a796e/41af167f-941a-4d42-ad21-32a5db11a1a9/grilled_chicken_with_spicy_lime.jpg",
                             Description = "Savory minced meat cooked with mixed vegetables.",
                             SubcategoryId = 4,
                             IngredientId = 3,
                             DefaultQuantityGrams = 150,
                             MealType =MealType.Protien,
                             AcceptCarb  = false
                },
                    new Meal
                    {
                                Name = "Whitefish fillet with tahini",
                                ImageUrl = "https://ichef.bbci.co.uk/food/ic/food_16x9_1600/recipes/air_fryer_roast_chicken_27390_16x9.jpg",
                                Description = "Savory minced meat cooked with mixed vegetables.",
                                SubcategoryId = 5,
                                IngredientId = 10,
                                DefaultQuantityGrams = 150,
                                MealType =MealType.Protien,
                                AcceptCarb  = true
                    },
                    new Meal
                    {
                                Name = "Shrimp Fajita",
                                ImageUrl = "https://images.unsplash.com/photo-1678684279246-96e6afb970f2?q=80&w=1223&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                Description = "Delicious grilled shrimp with garlic and herbs.",
                                SubcategoryId = 5,
                                IngredientId = 11,
                                DefaultQuantityGrams = 150,
                                MealType =MealType.Protien,
                                AcceptCarb  = true
                    },
                    new Meal
                    {
                                Name = "Whole Chicken Ranch Pizza",
                                ImageUrl = "https://www.vindulge.com/wp-content/uploads/2023/02/Pizza-with-Jalapeno-Coppa-and-Hot-Honey.jpg",
                                Description = "Tender baked fish fillet with lemon and dill.",
                                SubcategoryId = 6,
                                FixedCalories = 300,
                                FixedProtein = 25,
                                FixedCarbs = 30,
                                FixedFats = 10,
                                MealType =MealType.Protien,
                                AcceptCarb  = false
                    },
                    new Meal
                    {
                                Name = "Whole Tuna Ranch Pizza",
                                ImageUrl = "https://staticcookist.akamaized.net/wp-content/uploads/sites/22/2019/08/pizza-1200x675.jpg?im=Resize,width=570;",
                                Description = "Traditional Egyptian hawawshi stuffed with spiced minced meat.",
                                SubcategoryId = 6,
                                FixedCalories =1300,
                                FixedProtein = 56,
                                FixedCarbs = 47,
                                FixedFats = 31,
                                MealType =MealType.Protien,
                                AcceptCarb  = false
                    },
                    new Meal
                    {
                                Name = "Hawawshi with minced meat and mozzarella",
                                ImageUrl = "https://staticcookist.akamaized.net/wp-content/uploads/sites/22/2019/08/pizza-1200x675.jpg?im=Resize,width=570;",
                                Description = "Flavorful rice cooked with mixed vegetables and grilled chicken.",
                                SubcategoryId = 7,
                                FixedCalories =2300,
                                FixedProtein = 58,
                                FixedCarbs = 347,
                                FixedFats = 14,
                                MealType =MealType.Protien,
                                AcceptCarb  = false
                    },
                    new Meal
                    {
                                Name = "Basmati rice with vegetables",
                                ImageUrl = "https://www.lemonblossoms.com/wp-content/uploads/2019/11/How-To-Cook-White-Rice-S1-1.jpg",
                                Description = "Savory rice cooked with mixed vegetables and tender beef.",
                                SubcategoryId = 8,
                                IngredientId = 13,
                                DefaultQuantityGrams = 200,
                                MealType =MealType.Carb,
                                AcceptCarb  = false
                    },
                    new Meal
                    {
                                Name = "macaroni with bechamel",
                                ImageUrl = "https://cdn77-s3.lazycatkitchen.com/wp-content/uploads/2021/10/roasted-tomato-sauce-portion-800x1200.jpg",
                                Description = "Whole wheat pasta tossed in a rich tomato sauce with grilled chicken.",
                                SubcategoryId = 9,
                                IngredientId = 13,
                                DefaultQuantityGrams = 200,
                                MealType =MealType.Carb,
                                AcceptCarb  = false
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
                                MealType =MealType.BreakFastAndDinner,
                                AcceptCarb  = false
                    },
                    new Meal
                    {
                                Name = "Chicken Cheese Roll",
                                ImageUrl = "https://images.services.kitchenstories.io/aJycJxbZPvDuWtlILdg7WiJfomY=/1920x0/filters:quality(85)/images.kitchenstories.io/communityImages/f4604e05f6a9eaca99afddd69e849005_c02485d4-0841-4de6-b152-69deb38693f2.jpg",
                                Description = "Crisp romaine lettuce with grilled chicken, parmesan, and Caesar dressing.",
                                SubcategoryId = 11,
                                FixedCalories = 350,
                                FixedProtein = 30,
                                FixedCarbs = 15,
                                FixedFats = 12,
                                MealType =MealType.BreakFastAndDinner,
                                AcceptCarb  = false
                    },
                    new Meal
                    {
                                Name = "Mushroom egg toast",
                                ImageUrl = "https://assets.epicurious.com/photos/5c4b7ab537d8ef4605419f1d/1:1/w_1920,c_limit/St.-Patrick's-Day-Breakfast-Hash-012319.jpg",
                                Description = "Sliced turkey and creamy avocado wrapped in a whole wheat tortilla.",
                                SubcategoryId = 12,
                                FixedCalories = 320,
                                FixedProtein = 28,
                                FixedCarbs = 18,
                                FixedFats = 10,
                                MealType =MealType.BreakFastAndDinner,
                                AcceptCarb  = false
                    },
                    new Meal
                    {
                                Name = "Tuna Ranch Slides Pizza",
                                ImageUrl = "https://images.services.kitchenstories.io/aJycJxbZPvDuWtlILdg7WiJfomY=/1920x0/filters:quality(85)/images.kitchenstories.io/communityImages/f4604e05f6a9eaca99afddd69e849005_c02485d4-0841-4de6-b152-69deb38693f2.jpg",
                                Description = "Classic margherita pizza with fresh tomatoes, mozzarella, and basil.",
                                SubcategoryId = 13,
                                FixedCalories = 450,
                                FixedProtein = 20,
                                FixedCarbs = 50,
                                FixedFats = 15,
                                MealType =MealType.BreakFastAndDinner,
                                AcceptCarb  = false
                    },
                    new Meal
                    {
                                Name = "Omelette with vegetables",
                                ImageUrl = "https://assets.epicurious.com/photos/5c4b7ab537d8ef4605419f1d/1:1/w_1920,c_limit/St.-Patrick's-Day-Breakfast-Hash-012319.jpg",
                                Description = "Fluffy scrambled eggs cooked with fresh vegetables.",
                                SubcategoryId = 14,
                                FixedCalories = 250,
                                FixedProtein = 20,
                                FixedCarbs = 10,
                                FixedFats = 15,
                                MealType =MealType.BreakFastAndDinner,
                                AcceptCarb  = false
                    },
                    new Meal
                    {
                                Name = "Sugar-free chocolate mousse",
                                ImageUrl = "https://images.services.kitchenstories.io/aJycJxbZPvDuWtlILdg7WiJfomY=/1920x0/filters:quality(85)/images.kitchenstories.io/communityImages/f4604e05f6a9eaca99afddd69e849005_c02485d4-0841-4de6-b152-69deb38693f2.jpg",
                                Description = "Decadent chocolate mousse made with sugar substitutes.",
                                SubcategoryId = 15,
                                FixedCalories = 200,
                                FixedProtein = 5,
                                FixedCarbs = 15,
                                FixedFats = 12,
                                MealType =MealType.BreakFastAndDinner,
                                AcceptCarb  = false
                    }

        };

        await context.Meals.AddRangeAsync(meals);
        await context.SaveChangesAsync();
    }
}
