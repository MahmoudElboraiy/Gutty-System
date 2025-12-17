using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Seeding.Foods
{
    public static class SeedPlan
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Plans.AnyAsync())
                return;

            var basicPlanId = Guid.NewGuid();

            var plans = new List<Plan>
           {
            new Plan
            {
                Id = basicPlanId,
                Name = "Lunch Meal Per Month",
                Description = "That is the best and most healthy Packdage",
                DurationInDays = 28,
                LMealsPerDay = 1,
                BDMealsPerDay =2,
                BreakfastPrice = 2000,
                DinnerPrice = 2000,
                CarbGrams = 250,
                MaxCarbGrams = 400,
                LunchCategories = new List<PlanCategory>
                {
                    new PlanCategory
                    {
                      //  Id =1,
                        PlanId = basicPlanId,
                        Name = "Chicken Menu",
                        NumberOfMeals = 4,
                        ProteinGrams = 120,
                        PricePerGram = 2m,
                        AllowProteinChange = true,
                        MaxProteinGrams = 250
                    },                  
                    new PlanCategory
                    {
                       // Id = 2,
                        PlanId = basicPlanId,
                        Name = "Chicken Twagen",
                        NumberOfMeals = 4,
                        ProteinGrams = 100,
                        PricePerGram = 1.5m,
                        AllowProteinChange = true,
                        MaxProteinGrams = 230
                    },
                    new PlanCategory
                    {
                      //  Id = 3,
                        PlanId = basicPlanId,
                        Name = "Meat Menu",
                        NumberOfMeals = 4,
                        ProteinGrams = 100,
                        PricePerGram = 1.5m,
                        AllowProteinChange = true,
                        MaxProteinGrams = 230
                    },
                    new PlanCategory
                    {
                      //  Id = 4,
                        PlanId = basicPlanId,
                        Name = "Twagen Meat",
                        NumberOfMeals = 4,
                        ProteinGrams = 100,
                        PricePerGram = 2m,
                        AllowProteinChange = true,
                        MaxProteinGrams = 230
                    },
                    new PlanCategory
                    {
                      //  Id =5,
                        PlanId = basicPlanId,
                        Name = "Sea Food Menu",
                        NumberOfMeals = 4,
                        ProteinGrams = 100,
                        PricePerGram = 2m,
                        AllowProteinChange = true,
                        MaxProteinGrams = 230
                    },
                    new PlanCategory
                    {
                      //  Id = 6,
                        PlanId = basicPlanId,
                        Name = "Pizaa Menu",
                        NumberOfMeals = 4,
                        ProteinGrams = 100,
                        PricePerGram = 2m,
                        AllowProteinChange = false,
                        MaxProteinGrams = 230
                    },
                    new PlanCategory
                    {
                        //Id = 7,
                        PlanId = basicPlanId,
                        Name = "Hawawshi Menu",
                        NumberOfMeals = 4,
                        ProteinGrams = 100,
                        PricePerGram = 2m,
                        AllowProteinChange = false,
                        MaxProteinGrams = 230
                    }
                }
            }
        };
            //var plans = new List<Plan>
            //{
            //    new Plan
            //    {
            //        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            //        Name = "Main Meal Plan",
            //        Description = "قائمة أسعار الباكج الشهري (4 أسابيع) - وجبة غذاء رئيسية",
            //        DurationInDays = 28,

            //    },
            //new Plan
            //{
            //    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            //    Name = "Breakfast + Main Meal Plan",
            //    Description = "وجبة فطار + وجبة غذاء رئيسية",
            //    DurationInDays = 28,
            //    LunchMealsPerDay = 1,
            //    DinnerMealsPerDay = 0,
            //    BreakfastMealsPerDay = 1,
            //    MaxSeaFood = 6,
            //    MaxMeat = 6,
            //    MaxTwagen = 6,
            //    MaxChicken = 12,
            //    MaxPizza = 4,
            //    MaxHighCarb = 10,
            //    Price = 7500.00m
            //},
            //new Plan
            //{
            //    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            //    Name = "Full Day Plan",
            //    Description = "وجبة فطار + وجبة غذاء رئيسية + وجبة عشاء",
            //    DurationInDays = 28,
            //    LunchMealsPerDay = 1,
            //    DinnerMealsPerDay = 1,
            //    BreakfastMealsPerDay = 1,
            //    MaxSeaFood = 8,
            //    MaxMeat = 8,
            //    MaxTwagen = 8,
            //    MaxChicken = 16,
            //    MaxPizza = 6,
            //    MaxHighCarb = 14,
            //    Price = 8500.00m
            //},
            //new Plan
            //{
            //    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            //    Name = "Two Main Meals Plan",
            //    Description = "وجبتين غذاء رئيستين",
            //    DurationInDays = 28,
            //    LunchMealsPerDay = 2,
            //    DinnerMealsPerDay = 0,
            //    BreakfastMealsPerDay = 0,
            //    MaxSeaFood = 8,
            //    MaxMeat = 8,
            //    MaxTwagen = 8,
            //    MaxChicken = 16,
            //    MaxPizza = 4,
            //    MaxHighCarb = 12,
            //    Price = 9000.00m
            //},
            //new Plan
            //{
            //    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            //    Name = "Two Main Meals + Breakfast Plan",
            //    Description = "وجبتين غذاء رئيستين + وجبة فطار",
            //    DurationInDays = 28,
            //    LunchMealsPerDay = 2,
            //    DinnerMealsPerDay = 0,
            //    BreakfastMealsPerDay = 1,
            //    MaxSeaFood = 10,
            //    MaxMeat = 10,
            //    MaxTwagen = 10,
            //    MaxChicken = 20,
            //    MaxPizza = 6,
            //    MaxHighCarb = 16,
            //    Price = 10000.00m
            //},
            //new Plan
            //{
            //    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
            //    Name = "Premium Full Plan",
            //    Description = "وجبتين غذاء رئيستين + وجبة فطار + وجبة عشاء",
            //    DurationInDays = 28,
            //    LunchMealsPerDay = 2,
            //    DinnerMealsPerDay = 1,
            //    BreakfastMealsPerDay = 1,
            //    MaxSeaFood = 12,
            //    MaxMeat = 12,
            //    MaxTwagen = 12,
            //    MaxChicken = 24,
            //    MaxPizza = 8,
            //    MaxHighCarb = 20,
            //    Price = 11000.00m
            //}
       // };

            await context.Plans.AddRangeAsync(plans);
            await context.SaveChangesAsync();
        }
    }
} 