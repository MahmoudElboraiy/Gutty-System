using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Seeding.Foods
{
    public static class SeedItemIngredient
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.ItemIngredients.AnyAsync())
                return;

            var itemIngredients = new List<ItemIngredient>
            {
                    // Grilled Chicken Breast ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("4c9db05c-9caa-4044-ad7e-28f987817609"),
                        IngredientId = 6, // Chicken Breast
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("4c9db05c-9caa-4044-ad7e-28f987817609"),
                        IngredientId = 30, // Herbs
                        Quantity = 10.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("4c9db05c-9caa-4044-ad7e-28f987817609"),
                        IngredientId = 11, // Olive Oil
                        Quantity = 10.0m,
                        IsOptional = false
                    },

                    // Grilled Piccata Breast ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("5d9db05c-acaa-4044-ad7e-28f98781760a"),
                        IngredientId = 6, // Chicken Breast
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("5d9db05c-acaa-4044-ad7e-28f98781760a"),
                        IngredientId = 32, // Lemon
                        Quantity = 20.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("5d9db05c-acaa-4044-ad7e-28f98781760a"),
                        IngredientId = 33, // Parsley
                        Quantity = 15.0m,
                        IsOptional = false
                    },

                    // Grilled BBQ Breast ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("6e9db05c-bcaa-4044-ad7e-28f98781760b"),
                        IngredientId = 6, // Chicken Breast
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("6e9db05c-bcaa-4044-ad7e-28f98781760b"),
                        IngredientId = 31, // Spices
                        Quantity = 8.0m,
                        IsOptional = false
                    },

                    // Grilled Fajita Breast ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("7f9db05c-ccaa-4044-ad7e-28f98781760c"),
                        IngredientId = 6, // Chicken Breast
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("7f9db05c-ccaa-4044-ad7e-28f98781760c"),
                        IngredientId = 14, // Bell Peppers
                        Quantity = 50.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("7f9db05c-ccaa-4044-ad7e-28f98781760c"),
                        IngredientId = 12, // Onions
                        Quantity = 30.0m,
                        IsOptional = false
                    },

                    // Grilled Shish Taouk ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("8a9db05c-dcaa-4044-ad7e-28f98781760d"),
                        IngredientId = 6, // Chicken Breast
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("8a9db05c-dcaa-4044-ad7e-28f98781760d"),
                        IngredientId = 13, // Garlic
                        Quantity = 15.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("8a9db05c-dcaa-4044-ad7e-28f98781760d"),
                        IngredientId = 31, // Spices
                        Quantity = 10.0m,
                        IsOptional = false
                    },

                    // Chicken Roll ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("9b9db05c-ecaa-4044-ad7e-28f98781760e"),
                        IngredientId = 6, // Chicken Breast
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("9b9db05c-ecaa-4044-ad7e-28f98781760e"),
                        IngredientId = 30, // Herbs
                        Quantity = 12.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("9b9db05c-ecaa-4044-ad7e-28f98781760e"),
                        IngredientId = 10, // Cheese
                        Quantity = 25.0m,
                        IsOptional = true
                    },

                    // Smooth Chicken Tagine ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("aa9db05c-aaaa-4044-ad7e-28f987817620"),
                        IngredientId = 6, // Chicken Breast
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("aa9db05c-aaaa-4044-ad7e-28f987817620"),
                        IngredientId = 12, // Onions
                        Quantity = 30.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("aa9db05c-aaaa-4044-ad7e-28f987817620"),
                        IngredientId = 31, // Spices
                        Quantity = 8.0m,
                        IsOptional = false
                    },

                    // Makoula Chicken Tagine ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("bb9db05c-bbbb-4044-ad7e-28f987817621"),
                        IngredientId = 6, // Chicken Breast
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("bb9db05c-bbbb-4044-ad7e-28f987817621"),
                        IngredientId = 17, // Potatoes
                        Quantity = 80.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("bb9db05c-bbbb-4044-ad7e-28f987817621"),
                        IngredientId = 8, // Tomatoes
                        Quantity = 60.0m,
                        IsOptional = false
                    },

                    // Sayeh Chicken Tagine ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("cc9db05c-cccc-4044-ad7e-28f987817622"),
                        IngredientId = 6, // Chicken Breast
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("cc9db05c-cccc-4044-ad7e-28f987817622"),
                        IngredientId = 19, // Zucchini
                        Quantity = 80.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("cc9db05c-cccc-4044-ad7e-28f987817622"),
                        IngredientId = 16, // Carrots
                        Quantity = 60.0m,
                        IsOptional = false
                    },

                    // Moussaka Chicken Tagine ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("dd9db05c-dddd-4044-ad7e-28f987817623"),
                        IngredientId = 6, // Chicken Breast
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("dd9db05c-dddd-4044-ad7e-28f987817623"),
                        IngredientId = 18, // Eggplant
                        Quantity = 100.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("dd9db05c-dddd-4044-ad7e-28f987817623"),
                        IngredientId = 8, // Tomatoes
                        Quantity = 80.0m,
                        IsOptional = false
                    },

                    // Mixed Vegetables Chicken Tagine ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("ee9db05c-eeee-4044-ad7e-28f987817624"),
                        IngredientId = 6, // Chicken Breast
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("ee9db05c-eeee-4044-ad7e-28f987817624"),
                        IngredientId = 14, // Bell Peppers
                        Quantity = 60.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("ee9db05c-eeee-4044-ad7e-28f987817624"),
                        IngredientId = 19, // Zucchini
                        Quantity = 50.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("ee9db05c-eeee-4044-ad7e-28f987817624"),
                        IngredientId = 16, // Carrots
                        Quantity = 40.0m,
                        IsOptional = false
                    },

                    // Mediterranean Quinoa Bowl ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("ff9db05c-ffff-4044-ad7e-28f987817625"),
                        IngredientId = 22, // Quinoa
                        Quantity = 200.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("ff9db05c-ffff-4044-ad7e-28f987817625"),
                        IngredientId = 15, // Cucumber
                        Quantity = 80.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("ff9db05c-ffff-4044-ad7e-28f987817625"),
                        IngredientId = 8, // Tomatoes
                        Quantity = 100.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("ff9db05c-ffff-4044-ad7e-28f987817625"),
                        IngredientId = 11, // Olive Oil
                        Quantity = 20.0m,
                        IsOptional = false
                    },

                    // Salmon Teriyaki ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("119db05c-1111-4044-ad7e-28f987817626"),
                        IngredientId = 24, // Salmon
                        Quantity = 200.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("119db05c-1111-4044-ad7e-28f987817626"),
                        IngredientId = 35, // Ginger
                        Quantity = 10.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("119db05c-1111-4044-ad7e-28f987817626"),
                        IngredientId = 13, // Garlic
                        Quantity = 8.0m,
                        IsOptional = false
                    },

                    // Beef Stir Fry ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("229db05c-2222-4044-ad7e-28f987817627"),
                        IngredientId = 25, // Beef
                        Quantity = 200.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("229db05c-2222-4044-ad7e-28f987817627"),
                        IngredientId = 14, // Bell Peppers
                        Quantity = 80.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("229db05c-2222-4044-ad7e-28f987817627"),
                        IngredientId = 12, // Onions
                        Quantity = 60.0m,
                        IsOptional = false
                    },

                    // Vegetarian Buddha Bowl ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("339db05c-3333-4044-ad7e-28f987817628"),
                        IngredientId = 22, // Quinoa
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("339db05c-3333-4044-ad7e-28f987817628"),
                        IngredientId = 23, // Chickpeas
                        Quantity = 120.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("339db05c-3333-4044-ad7e-28f987817628"),
                        IngredientId = 20, // Spinach
                        Quantity = 100.0m,
                        IsOptional = false
                    },

                    // Turkey Meatballs ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("449db05c-4444-4044-ad7e-28f987817629"),
                        IngredientId = 26, // Turkey
                        Quantity = 200.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("449db05c-4444-4044-ad7e-28f987817629"),
                        IngredientId = 8, // Tomatoes
                        Quantity = 100.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("449db05c-4444-4044-ad7e-28f987817629"),
                        IngredientId = 13, // Garlic
                        Quantity = 10.0m,
                        IsOptional = false
                    },

                    // Greek Yogurt Parfait ingredients
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("559db05c-5555-4044-ad7e-28f98781762a"),
                        IngredientId = 27, // Greek Yogurt
                        Quantity = 150.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("559db05c-5555-4044-ad7e-28f98781762a"),
                        IngredientId = 28, // Berries
                        Quantity = 80.0m,
                        IsOptional = false
                    },
                    new ItemIngredient
                    {
                        ItemId = Guid.Parse("559db05c-5555-4044-ad7e-28f98781762a"),
                        IngredientId = 29, // Granola
                        Quantity = 50.0m,
                        IsOptional = false
                    },

                    // Mixed Vegetables Chicken Tagine ingredients
                    new ItemIngredient { ItemId = Guid.Parse("ee9db05c-eeee-4044-ad7e-28f987817624"), IngredientId = 6, IsOptional = false, Quantity = 150.0m }, // Chicken Breast
                    new ItemIngredient { ItemId = Guid.Parse("ee9db05c-eeee-4044-ad7e-28f987817624"), IngredientId = 14, IsOptional = false, Quantity = 30.0m }, // Bell Peppers
                    new ItemIngredient { ItemId = Guid.Parse("ee9db05c-eeee-4044-ad7e-28f987817624"), IngredientId = 16, IsOptional = false, Quantity = 25.0m }, // Carrots
                    new ItemIngredient { ItemId = Guid.Parse("ee9db05c-eeee-4044-ad7e-28f987817624"), IngredientId = 19, IsOptional = false, Quantity = 25.0m }, // Zucchini
                    new ItemIngredient { ItemId = Guid.Parse("ee9db05c-eeee-4044-ad7e-28f987817624"), IngredientId = 11, IsOptional = false, Quantity = 10.0m }, // Olive Oil
                    new ItemIngredient { ItemId = Guid.Parse("ee9db05c-eeee-4044-ad7e-28f987817624"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Mediterranean Quinoa Bowl ingredients
                    new ItemIngredient { ItemId = Guid.Parse("ff9db05c-ffff-4044-ad7e-28f987817625"), IngredientId = 22, IsOptional = false, Quantity = 150.0m }, // Quinoa
                    new ItemIngredient { ItemId = Guid.Parse("ff9db05c-ffff-4044-ad7e-28f987817625"), IngredientId = 8, IsOptional = false, Quantity = 50.0m }, // Tomatoes
                    new ItemIngredient { ItemId = Guid.Parse("ff9db05c-ffff-4044-ad7e-28f987817625"), IngredientId = 15, IsOptional = false, Quantity = 30.0m }, // Cucumber
                    new ItemIngredient { ItemId = Guid.Parse("ff9db05c-ffff-4044-ad7e-28f987817625"), IngredientId = 12, IsOptional = false, Quantity = 20.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("ff9db05c-ffff-4044-ad7e-28f987817625"), IngredientId = 11, IsOptional = false, Quantity = 15.0m }, // Olive Oil

                    // Salmon Teriyaki ingredients
                    new ItemIngredient { ItemId = Guid.Parse("119db05c-1111-4044-ad7e-28f987817626"), IngredientId = 24, IsOptional = false, Quantity = 200.0m }, // Salmon
                    new ItemIngredient { ItemId = Guid.Parse("119db05c-1111-4044-ad7e-28f987817626"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices
                    new ItemIngredient { ItemId = Guid.Parse("119db05c-1111-4044-ad7e-28f987817626"), IngredientId = 35, IsOptional = false, Quantity = 3.0m }, // Ginger

                    // Beef Stir Fry ingredients
                    new ItemIngredient { ItemId = Guid.Parse("229db05c-2222-4044-ad7e-28f987817627"), IngredientId = 25, IsOptional = false, Quantity = 200.0m }, // Beef
                    new ItemIngredient { ItemId = Guid.Parse("229db05c-2222-4044-ad7e-28f987817627"), IngredientId = 14, IsOptional = false, Quantity = 50.0m }, // Bell Peppers
                    new ItemIngredient { ItemId = Guid.Parse("229db05c-2222-4044-ad7e-28f987817627"), IngredientId = 12, IsOptional = false, Quantity = 30.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("229db05c-2222-4044-ad7e-28f987817627"), IngredientId = 11, IsOptional = false, Quantity = 15.0m }, // Olive Oil

                    // Vegetarian Buddha Bowl ingredients
                    new ItemIngredient { ItemId = Guid.Parse("339db05c-3333-4044-ad7e-28f987817628"), IngredientId = 22, IsOptional = false, Quantity = 100.0m }, // Quinoa
                    new ItemIngredient { ItemId = Guid.Parse("339db05c-3333-4044-ad7e-28f987817628"), IngredientId = 23, IsOptional = false, Quantity = 80.0m }, // Chickpeas
                    new ItemIngredient { ItemId = Guid.Parse("339db05c-3333-4044-ad7e-28f987817628"), IngredientId = 20, IsOptional = false, Quantity = 50.0m }, // Spinach
                    new ItemIngredient { ItemId = Guid.Parse("339db05c-3333-4044-ad7e-28f987817628"), IngredientId = 16, IsOptional = false, Quantity = 40.0m }, // Carrots
                    new ItemIngredient { ItemId = Guid.Parse("339db05c-3333-4044-ad7e-28f987817628"), IngredientId = 11, IsOptional = false, Quantity = 15.0m }, // Olive Oil

                    // Turkey Meatballs ingredients
                    new ItemIngredient { ItemId = Guid.Parse("449db05c-4444-4044-ad7e-28f987817629"), IngredientId = 26, IsOptional = false, Quantity = 200.0m }, // Turkey
                    new ItemIngredient { ItemId = Guid.Parse("449db05c-4444-4044-ad7e-28f987817629"), IngredientId = 8, IsOptional = false, Quantity = 30.0m }, // Tomatoes
                    new ItemIngredient { ItemId = Guid.Parse("449db05c-4444-4044-ad7e-28f987817629"), IngredientId = 12, IsOptional = false, Quantity = 20.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("449db05c-4444-4044-ad7e-28f987817629"), IngredientId = 13, IsOptional = false, Quantity = 5.0m }, // Garlic

                    // Greek Yogurt Parfait ingredients
                    new ItemIngredient { ItemId = Guid.Parse("559db05c-5555-4044-ad7e-28f98781762a"), IngredientId = 27, IsOptional = false, Quantity = 150.0m }, // Greek Yogurt
                    new ItemIngredient { ItemId = Guid.Parse("559db05c-5555-4044-ad7e-28f98781762a"), IngredientId = 28, IsOptional = false, Quantity = 30.0m }, // Berries
                    new ItemIngredient { ItemId = Guid.Parse("559db05c-5555-4044-ad7e-28f98781762a"), IngredientId = 29, IsOptional = false, Quantity = 20.0m }, // Granola

                    // Beef Piccata Tagine ingredients
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f98781762b"), IngredientId = 36, IsOptional = false, Quantity = 100.0m }, // Ground Beef
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f98781762b"), IngredientId = 32, IsOptional = false, Quantity = 10.0m }, // Lemon
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f98781762b"), IngredientId = 30, IsOptional = false, Quantity = 5.0m }, // Herbs
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f98781762b"), IngredientId = 11, IsOptional = false, Quantity = 5.0m }, // Olive Oil

                    // Sirloin BBQ ingredients
                    new ItemIngredient { ItemId = Guid.Parse("770db05c-7777-4044-ad7e-28f98781762c"), IngredientId = 25, IsOptional = false, Quantity = 100.0m }, // Beef
                    new ItemIngredient { ItemId = Guid.Parse("770db05c-7777-4044-ad7e-28f98781762c"), IngredientId = 45, IsOptional = false, Quantity = 15.0m }, // BBQ Sauce
                    new ItemIngredient { ItemId = Guid.Parse("770db05c-7777-4044-ad7e-28f98781762c"), IngredientId = 31, IsOptional = false, Quantity = 3.0m }, // Spices

                    // Ground Meat ingredients
                    new ItemIngredient { ItemId = Guid.Parse("880db05c-8888-4044-ad7e-28f98781762d"), IngredientId = 36, IsOptional = false, Quantity = 100.0m }, // Ground Beef
                    new ItemIngredient { ItemId = Guid.Parse("880db05c-8888-4044-ad7e-28f98781762d"), IngredientId = 12, IsOptional = false, Quantity = 10.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("880db05c-8888-4044-ad7e-28f98781762d"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Beef Meatballs ingredients
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781762e"), IngredientId = 36, IsOptional = false, Quantity = 100.0m }, // Ground Beef
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781762e"), IngredientId = 4, IsOptional = false, Quantity = 5.0m }, // Eggs
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781762e"), IngredientId = 1, IsOptional = false, Quantity = 10.0m }, // Flour
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781762e"), IngredientId = 31, IsOptional = false, Quantity = 3.0m }, // Spices

                    // Tahini Meatballs ingredients
                    new ItemIngredient { ItemId = Guid.Parse("aa0db05c-aaaa-4044-ad7e-28f98781762f"), IngredientId = 36, IsOptional = false, Quantity = 100.0m }, // Ground Beef
                    new ItemIngredient { ItemId = Guid.Parse("aa0db05c-aaaa-4044-ad7e-28f98781762f"), IngredientId = 42, IsOptional = false, Quantity = 15.0m }, // Tahini
                    new ItemIngredient { ItemId = Guid.Parse("aa0db05c-aaaa-4044-ad7e-28f98781762f"), IngredientId = 31, IsOptional = false, Quantity = 3.0m }, // Spices

                    // Beef Burger ingredients
                    new ItemIngredient { ItemId = Guid.Parse("bb0db05c-bbbb-4044-ad7e-28f987817630"), IngredientId = 36, IsOptional = false, Quantity = 100.0m }, // Ground Beef
                    new ItemIngredient { ItemId = Guid.Parse("bb0db05c-bbbb-4044-ad7e-28f987817630"), IngredientId = 12, IsOptional = false, Quantity = 10.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("bb0db05c-bbbb-4044-ad7e-28f987817630"), IngredientId = 31, IsOptional = false, Quantity = 3.0m }, // Spices

                    // Smooth Meat Tagine ingredients
                    new ItemIngredient { ItemId = Guid.Parse("cc0db05c-cccc-4044-ad7e-28f987817631"), IngredientId = 25, IsOptional = false, Quantity = 100.0m }, // Beef
                    new ItemIngredient { ItemId = Guid.Parse("cc0db05c-cccc-4044-ad7e-28f987817631"), IngredientId = 8, IsOptional = false, Quantity = 15.0m }, // Tomatoes
                    new ItemIngredient { ItemId = Guid.Parse("cc0db05c-cccc-4044-ad7e-28f987817631"), IngredientId = 12, IsOptional = false, Quantity = 10.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("cc0db05c-cccc-4044-ad7e-28f987817631"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Molokhia Meat Tagine ingredients
                    new ItemIngredient { ItemId = Guid.Parse("dd0db05c-dddd-4044-ad7e-28f987817632"), IngredientId = 25, IsOptional = false, Quantity = 100.0m }, // Beef
                    new ItemIngredient { ItemId = Guid.Parse("dd0db05c-dddd-4044-ad7e-28f987817632"), IngredientId = 47, IsOptional = false, Quantity = 20.0m }, // Molokhia
                    new ItemIngredient { ItemId = Guid.Parse("dd0db05c-dddd-4044-ad7e-28f987817632"), IngredientId = 13, IsOptional = false, Quantity = 5.0m }, // Garlic
                    new ItemIngredient { ItemId = Guid.Parse("dd0db05c-dddd-4044-ad7e-28f987817632"), IngredientId = 31, IsOptional = false, Quantity = 3.0m }, // Spices

                    // Sayeh Meat Tagine ingredients
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817633"), IngredientId = 25, IsOptional = false, Quantity = 100.0m }, // Beef
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817633"), IngredientId = 14, IsOptional = false, Quantity = 15.0m }, // Bell Peppers
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817633"), IngredientId = 12, IsOptional = false, Quantity = 10.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817633"), IngredientId = 31, IsOptional = false, Quantity = 3.0m }, // Spices

                    // Moussaka Meat Tagine ingredients
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817634"), IngredientId = 25, IsOptional = false, Quantity = 100.0m }, // Beef
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817634"), IngredientId = 18, IsOptional = false, Quantity = 30.0m }, // Eggplant
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817634"), IngredientId = 8, IsOptional = false, Quantity = 20.0m }, // Tomatoes
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817634"), IngredientId = 31, IsOptional = false, Quantity = 3.0m }, // Spices

                    // Mixed Vegetables Meat Tagine ingredients
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817635"), IngredientId = 25, IsOptional = false, Quantity = 100.0m }, // Beef
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817635"), IngredientId = 14, IsOptional = false, Quantity = 20.0m }, // Bell Peppers
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817635"), IngredientId = 16, IsOptional = false, Quantity = 15.0m }, // Carrots
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817635"), IngredientId = 19, IsOptional = false, Quantity = 15.0m }, // Zucchini
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817635"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // White Fish Fillet with Tahini ingredients
                    new ItemIngredient { ItemId = Guid.Parse("220db05c-2222-4044-ad7e-28f987817636"), IngredientId = 37, IsOptional = false, Quantity = 120.0m }, // White Fish Fillet
                    new ItemIngredient { ItemId = Guid.Parse("220db05c-2222-4044-ad7e-28f987817636"), IngredientId = 42, IsOptional = false, Quantity = 20.0m }, // Tahini
                    new ItemIngredient { ItemId = Guid.Parse("220db05c-2222-4044-ad7e-28f987817636"), IngredientId = 32, IsOptional = false, Quantity = 5.0m }, // Lemon
                    new ItemIngredient { ItemId = Guid.Parse("220db05c-2222-4044-ad7e-28f987817636"), IngredientId = 30, IsOptional = false, Quantity = 3.0m }, // Herbs

                    // Fish Fillet with Bechamel ingredients
                    new ItemIngredient { ItemId = Guid.Parse("330db05c-3333-4044-ad7e-28f987817637"), IngredientId = 37, IsOptional = false, Quantity = 120.0m }, // White Fish Fillet
                    new ItemIngredient { ItemId = Guid.Parse("330db05c-3333-4044-ad7e-28f987817637"), IngredientId = 43, IsOptional = false, Quantity = 30.0m }, // Bechamel Sauce
                    new ItemIngredient { ItemId = Guid.Parse("330db05c-3333-4044-ad7e-28f987817637"), IngredientId = 40, IsOptional = false, Quantity = 15.0m }, // Mozzarella Cheese

                    // Fish Fillet with Lemon ingredients
                    new ItemIngredient { ItemId = Guid.Parse("440db05c-4444-4044-ad7e-28f987817638"), IngredientId = 37, IsOptional = false, Quantity = 120.0m }, // White Fish Fillet
                    new ItemIngredient { ItemId = Guid.Parse("440db05c-4444-4044-ad7e-28f987817638"), IngredientId = 32, IsOptional = false, Quantity = 15.0m }, // Lemon
                    new ItemIngredient { ItemId = Guid.Parse("440db05c-4444-4044-ad7e-28f987817638"), IngredientId = 30, IsOptional = false, Quantity = 3.0m }, // Herbs

                    // Loafa with Lemon and Tahini ingredients
                    new ItemIngredient { ItemId = Guid.Parse("550db05c-5555-4044-ad7e-28f987817639"), IngredientId = 37, IsOptional = false, Quantity = 120.0m }, // White Fish Fillet (Loafa)
                    new ItemIngredient { ItemId = Guid.Parse("550db05c-5555-4044-ad7e-28f987817639"), IngredientId = 32, IsOptional = false, Quantity = 10.0m }, // Lemon
                    new ItemIngredient { ItemId = Guid.Parse("550db05c-5555-4044-ad7e-28f987817639"), IngredientId = 42, IsOptional = false, Quantity = 20.0m }, // Tahini
                    new ItemIngredient { ItemId = Guid.Parse("550db05c-5555-4044-ad7e-28f987817639"), IngredientId = 30, IsOptional = false, Quantity = 5.0m }, // Herbs

                    // Shrimp Fajita ingredients
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f98781763a"), IngredientId = 38, IsOptional = false, Quantity = 120.0m }, // Shrimp
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f98781763a"), IngredientId = 14, IsOptional = false, Quantity = 20.0m }, // Bell Peppers
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f98781763a"), IngredientId = 12, IsOptional = false, Quantity = 10.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f98781763a"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Shrimp Bechamel ingredients
                    new ItemIngredient { ItemId = Guid.Parse("770db05c-7777-4044-ad7e-28f98781763b"), IngredientId = 38, IsOptional = false, Quantity = 120.0m }, // Shrimp
                    new ItemIngredient { ItemId = Guid.Parse("770db05c-7777-4044-ad7e-28f98781763b"), IngredientId = 43, IsOptional = false, Quantity = 40.0m }, // Bechamel Sauce
                    new ItemIngredient { ItemId = Guid.Parse("770db05c-7777-4044-ad7e-28f98781763b"), IngredientId = 40, IsOptional = false, Quantity = 20.0m }, // Mozzarella Cheese

                    // Complete Chicken Ranch Pizza ingredients
                    new ItemIngredient { ItemId = Guid.Parse("880db05c-8888-4044-ad7e-28f98781763c"), IngredientId = 41, IsOptional = false, Quantity = 200.0m }, // Pizza Dough
                    new ItemIngredient { ItemId = Guid.Parse("880db05c-8888-4044-ad7e-28f98781763c"), IngredientId = 6, IsOptional = false, Quantity = 150.0m }, // Chicken Breast
                    new ItemIngredient { ItemId = Guid.Parse("880db05c-8888-4044-ad7e-28f98781763c"), IngredientId = 44, IsOptional = false, Quantity = 30.0m }, // Ranch Sauce
                    new ItemIngredient { ItemId = Guid.Parse("880db05c-8888-4044-ad7e-28f98781763c"), IngredientId = 40, IsOptional = false, Quantity = 80.0m }, // Mozzarella Cheese

                    // Complete Chicken BBQ Pizza ingredients
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781763d"), IngredientId = 41, IsOptional = false, Quantity = 200.0m }, // Pizza Dough
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781763d"), IngredientId = 6, IsOptional = false, Quantity = 150.0m }, // Chicken Breast
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781763d"), IngredientId = 45, IsOptional = false, Quantity = 30.0m }, // BBQ Sauce
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781763d"), IngredientId = 40, IsOptional = false, Quantity = 80.0m }, // Mozzarella Cheese

                    // Complete Big Tasty Chicken Pizza ingredients
                    new ItemIngredient { ItemId = Guid.Parse("aa0db05c-aaaa-4044-ad7e-28f98781763e"), IngredientId = 41, IsOptional = false, Quantity = 200.0m }, // Pizza Dough
                    new ItemIngredient { ItemId = Guid.Parse("aa0db05c-aaaa-4044-ad7e-28f98781763e"), IngredientId = 6, IsOptional = false, Quantity = 150.0m }, // Chicken Breast
                    new ItemIngredient { ItemId = Guid.Parse("aa0db05c-aaaa-4044-ad7e-28f98781763e"), IngredientId = 40, IsOptional = false, Quantity = 80.0m }, // Mozzarella Cheese
                    new ItemIngredient { ItemId = Guid.Parse("aa0db05c-aaaa-4044-ad7e-28f98781763e"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Complete Big Tasty Burger Pizza ingredients
                    new ItemIngredient { ItemId = Guid.Parse("bb0db05c-bbbb-4044-ad7e-28f98781763f"), IngredientId = 41, IsOptional = false, Quantity = 200.0m }, // Pizza Dough
                    new ItemIngredient { ItemId = Guid.Parse("bb0db05c-bbbb-4044-ad7e-28f98781763f"), IngredientId = 36, IsOptional = false, Quantity = 150.0m }, // Ground Beef
                    new ItemIngredient { ItemId = Guid.Parse("bb0db05c-bbbb-4044-ad7e-28f98781763f"), IngredientId = 40, IsOptional = false, Quantity = 80.0m }, // Mozzarella Cheese
                    new ItemIngredient { ItemId = Guid.Parse("bb0db05c-bbbb-4044-ad7e-28f98781763f"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Complete Tuna Ranch Pizza ingredients
                    new ItemIngredient { ItemId = Guid.Parse("cc0db05c-cccc-4044-ad7e-28f987817640"), IngredientId = 41, IsOptional = false, Quantity = 200.0m }, // Pizza Dough
                    new ItemIngredient { ItemId = Guid.Parse("cc0db05c-cccc-4044-ad7e-28f987817640"), IngredientId = 39, IsOptional = false, Quantity = 150.0m }, // Tuna
                    new ItemIngredient { ItemId = Guid.Parse("cc0db05c-cccc-4044-ad7e-28f987817640"), IngredientId = 44, IsOptional = false, Quantity = 30.0m }, // Ranch Sauce
                    new ItemIngredient { ItemId = Guid.Parse("cc0db05c-cccc-4044-ad7e-28f987817640"), IngredientId = 40, IsOptional = false, Quantity = 80.0m }, // Mozzarella Cheese

                    // Complete Shrimp Ranch Pizza ingredients
                    new ItemIngredient { ItemId = Guid.Parse("dd0db05c-dddd-4044-ad7e-28f987817641"), IngredientId = 41, IsOptional = false, Quantity = 200.0m }, // Pizza Dough
                    new ItemIngredient { ItemId = Guid.Parse("dd0db05c-dddd-4044-ad7e-28f987817641"), IngredientId = 38, IsOptional = false, Quantity = 150.0m }, // Shrimp
                    new ItemIngredient { ItemId = Guid.Parse("dd0db05c-dddd-4044-ad7e-28f987817641"), IngredientId = 44, IsOptional = false, Quantity = 30.0m }, // Ranch Sauce
                    new ItemIngredient { ItemId = Guid.Parse("dd0db05c-dddd-4044-ad7e-28f987817641"), IngredientId = 40, IsOptional = false, Quantity = 80.0m }, // Mozzarella Cheese

                    // Minced Meat and Mozzarella Hawawshi ingredients
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817642"), IngredientId = 46, IsOptional = false, Quantity = 150.0m }, // Pita Bread
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817642"), IngredientId = 36, IsOptional = false, Quantity = 100.0m }, // Ground Beef
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817642"), IngredientId = 40, IsOptional = false, Quantity = 50.0m }, // Mozzarella Cheese
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817642"), IngredientId = 12, IsOptional = false, Quantity = 20.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817642"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Shish and Mozzarella Hawawshi ingredients
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817643"), IngredientId = 46, IsOptional = false, Quantity = 150.0m }, // Pita Bread
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817643"), IngredientId = 25, IsOptional = false, Quantity = 100.0m }, // Beef (Shish)
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817643"), IngredientId = 40, IsOptional = false, Quantity = 50.0m }, // Mozzarella Cheese
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817643"), IngredientId = 12, IsOptional = false, Quantity = 20.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817643"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Meat Kofta and Mozzarella Hawawshi ingredients
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817644"), IngredientId = 46, IsOptional = false, Quantity = 150.0m }, // Pita Bread
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817644"), IngredientId = 36, IsOptional = false, Quantity = 100.0m }, // Ground Beef (Kofta)
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817644"), IngredientId = 40, IsOptional = false, Quantity = 50.0m }, // Mozzarella Cheese
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817644"), IngredientId = 33, IsOptional = false, Quantity = 10.0m }, // Parsley
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817644"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Meat Burger and Mozzarella Hawawshi ingredients
                    new ItemIngredient { ItemId = Guid.Parse("220db05c-2222-4044-ad7e-28f987817645"), IngredientId = 46, IsOptional = false, Quantity = 150.0m }, // Pita Bread
                    new ItemIngredient { ItemId = Guid.Parse("220db05c-2222-4044-ad7e-28f987817645"), IngredientId = 36, IsOptional = false, Quantity = 100.0m }, // Ground Beef (Burger)
                    new ItemIngredient { ItemId = Guid.Parse("220db05c-2222-4044-ad7e-28f987817645"), IngredientId = 40, IsOptional = false, Quantity = 50.0m }, // Mozzarella Cheese
                    new ItemIngredient { ItemId = Guid.Parse("220db05c-2222-4044-ad7e-28f987817645"), IngredientId = 12, IsOptional = false, Quantity = 15.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("220db05c-2222-4044-ad7e-28f987817645"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Chicken Kofta and Mozzarella Hawawshi ingredients
                    new ItemIngredient { ItemId = Guid.Parse("330db05c-3333-4044-ad7e-28f987817646"), IngredientId = 46, IsOptional = false, Quantity = 150.0m }, // Pita Bread
                    new ItemIngredient { ItemId = Guid.Parse("330db05c-3333-4044-ad7e-28f987817646"), IngredientId = 48, IsOptional = false, Quantity = 100.0m }, // Ground Chicken (Kofta)
                    new ItemIngredient { ItemId = Guid.Parse("330db05c-3333-4044-ad7e-28f987817646"), IngredientId = 40, IsOptional = false, Quantity = 50.0m }, // Mozzarella Cheese
                    new ItemIngredient { ItemId = Guid.Parse("330db05c-3333-4044-ad7e-28f987817646"), IngredientId = 33, IsOptional = false, Quantity = 10.0m }, // Parsley
                    new ItemIngredient { ItemId = Guid.Parse("330db05c-3333-4044-ad7e-28f987817646"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Chicken Burger and Mozzarella Hawawshi ingredients
                    new ItemIngredient { ItemId = Guid.Parse("440db05c-4444-4044-ad7e-28f987817647"), IngredientId = 46, IsOptional = false, Quantity = 150.0m }, // Pita Bread
                    new ItemIngredient { ItemId = Guid.Parse("440db05c-4444-4044-ad7e-28f987817647"), IngredientId = 48, IsOptional = false, Quantity = 100.0m }, // Ground Chicken (Burger)
                    new ItemIngredient { ItemId = Guid.Parse("440db05c-4444-4044-ad7e-28f987817647"), IngredientId = 40, IsOptional = false, Quantity = 50.0m }, // Mozzarella Cheese
                    new ItemIngredient { ItemId = Guid.Parse("440db05c-4444-4044-ad7e-28f987817647"), IngredientId = 12, IsOptional = false, Quantity = 15.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("440db05c-4444-4044-ad7e-28f987817647"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Basmati Rice with Vegetables ingredients
                    new ItemIngredient { ItemId = Guid.Parse("550db05c-5555-4044-ad7e-28f987817648"), IngredientId = 49, IsOptional = false, Quantity = 200.0m }, // Basmati Rice
                    new ItemIngredient { ItemId = Guid.Parse("550db05c-5555-4044-ad7e-28f987817648"), IngredientId = 50, IsOptional = false, Quantity = 50.0m }, // Mixed Vegetables
                    new ItemIngredient { ItemId = Guid.Parse("550db05c-5555-4044-ad7e-28f987817648"), IngredientId = 11, IsOptional = false, Quantity = 10.0m }, // Olive Oil
                    new ItemIngredient { ItemId = Guid.Parse("550db05c-5555-4044-ad7e-28f987817648"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Basmati Rice Biryani ingredients
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f987817649"), IngredientId = 49, IsOptional = false, Quantity = 200.0m }, // Basmati Rice
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f987817649"), IngredientId = 51, IsOptional = false, Quantity = 10.0m }, // Biryani Spices
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f987817649"), IngredientId = 12, IsOptional = false, Quantity = 20.0m }, // Onions
                    new ItemIngredient { ItemId = Guid.Parse("660db05c-6666-4044-ad7e-28f987817649"), IngredientId = 11, IsOptional = false, Quantity = 10.0m }, // Olive Oil

                    // Basmati Rice Sayadia ingredients
                    new ItemIngredient { ItemId = Guid.Parse("770db05c-7777-4044-ad7e-28f98781764a"), IngredientId = 49, IsOptional = false, Quantity = 200.0m }, // Basmati Rice
                    new ItemIngredient { ItemId = Guid.Parse("770db05c-7777-4044-ad7e-28f98781764a"), IngredientId = 37, IsOptional = false, Quantity = 30.0m }, // White Fish Fillet
                    new ItemIngredient { ItemId = Guid.Parse("770db05c-7777-4044-ad7e-28f98781764a"), IngredientId = 31, IsOptional = false, Quantity = 8.0m }, // Spices
                    new ItemIngredient { ItemId = Guid.Parse("770db05c-7777-4044-ad7e-28f98781764a"), IngredientId = 11, IsOptional = false, Quantity = 10.0m }, // Olive Oil

                    // Baked Potato Fingers with Cheese ingredients
                    new ItemIngredient { ItemId = Guid.Parse("880db05c-8888-4044-ad7e-28f98781764b"), IngredientId = 17, IsOptional = false, Quantity = 200.0m }, // Potatoes
                    new ItemIngredient { ItemId = Guid.Parse("880db05c-8888-4044-ad7e-28f98781764b"), IngredientId = 10, IsOptional = false, Quantity = 30.0m }, // Cheese
                    new ItemIngredient { ItemId = Guid.Parse("880db05c-8888-4044-ad7e-28f98781764b"), IngredientId = 11, IsOptional = false, Quantity = 10.0m }, // Olive Oil
                    new ItemIngredient { ItemId = Guid.Parse("880db05c-8888-4044-ad7e-28f98781764b"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices

                    // Mashed Potato ingredients
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781764c"), IngredientId = 17, IsOptional = false, Quantity = 220.0m }, // Potatoes
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781764c"), IngredientId = 3, IsOptional = false, Quantity = 20.0m }, // Butter
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781764c"), IngredientId = 5, IsOptional = false, Quantity = 30.0m }, // Milk
                    new ItemIngredient { ItemId = Guid.Parse("990db05c-9999-4044-ad7e-28f98781764c"), IngredientId = 31, IsOptional = false, Quantity = 3.0m }, // Spices

                    // Potato Tagine with Bechamel ingredients
                    new ItemIngredient { ItemId = Guid.Parse("aa0db05c-aaaa-4044-ad7e-28f98781764d"), IngredientId = 17, IsOptional = false, Quantity = 200.0m }, // Potatoes
                    new ItemIngredient { ItemId = Guid.Parse("aa0db05c-aaaa-4044-ad7e-28f98781764d"), IngredientId = 43, IsOptional = false, Quantity = 40.0m }, // Bechamel Sauce
                    new ItemIngredient { ItemId = Guid.Parse("aa0db05c-aaaa-4044-ad7e-28f98781764d"), IngredientId = 10, IsOptional = false, Quantity = 25.0m }, // Cheese
                    new ItemIngredient { ItemId = Guid.Parse("aa0db05c-aaaa-4044-ad7e-28f98781764d"), IngredientId = 11, IsOptional = false, Quantity = 10.0m }, // Olive Oil

                    // Pasta Bechamel ingredients
                    new ItemIngredient { ItemId = Guid.Parse("bb0db05c-bbbb-4044-ad7e-28f98781764e"), IngredientId = 52, IsOptional = false, Quantity = 200.0m }, // Pasta
                    new ItemIngredient { ItemId = Guid.Parse("bb0db05c-bbbb-4044-ad7e-28f98781764e"), IngredientId = 43, IsOptional = false, Quantity = 60.0m }, // Bechamel Sauce
                    new ItemIngredient { ItemId = Guid.Parse("bb0db05c-bbbb-4044-ad7e-28f98781764e"), IngredientId = 40, IsOptional = false, Quantity = 40.0m }, // Mozzarella Cheese
                    new ItemIngredient { ItemId = Guid.Parse("bb0db05c-bbbb-4044-ad7e-28f98781764e"), IngredientId = 6, IsOptional = false, Quantity = 100.0m }, // Chicken Breast

                    // Fettuccine White Sauce ingredients
                    new ItemIngredient { ItemId = Guid.Parse("cc0db05c-cccc-4044-ad7e-28f98781764f"), IngredientId = 53, IsOptional = false, Quantity = 250.0m }, // Fettuccine
                    new ItemIngredient { ItemId = Guid.Parse("cc0db05c-cccc-4044-ad7e-28f98781764f"), IngredientId = 57, IsOptional = false, Quantity = 40.0m }, // White Sauce
                    new ItemIngredient { ItemId = Guid.Parse("cc0db05c-cccc-4044-ad7e-28f98781764f"), IngredientId = 30, IsOptional = false, Quantity = 5.0m }, // Herbs

                    // Mac and Cheese ingredients
                    new ItemIngredient { ItemId = Guid.Parse("dd0db05c-dddd-4044-ad7e-28f987817650"), IngredientId = 54, IsOptional = false, Quantity = 250.0m }, // Macaroni
                    new ItemIngredient { ItemId = Guid.Parse("dd0db05c-dddd-4044-ad7e-28f987817650"), IngredientId = 10, IsOptional = false, Quantity = 40.0m }, // Cheese
                    new ItemIngredient { ItemId = Guid.Parse("dd0db05c-dddd-4044-ad7e-28f987817650"), IngredientId = 5, IsOptional = false, Quantity = 30.0m }, // Milk

                    // Barbecue Logar ingredients
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817651"), IngredientId = 55, IsOptional = false, Quantity = 250.0m }, // Spaghetti
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817651"), IngredientId = 45, IsOptional = false, Quantity = 30.0m }, // BBQ Sauce
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817651"), IngredientId = 6, IsOptional = false, Quantity = 80.0m }, // Chicken Breast
                    new ItemIngredient { ItemId = Guid.Parse("ee0db05c-eeee-4044-ad7e-28f987817651"), IngredientId = 30, IsOptional = false, Quantity = 5.0m }, // Herbs

                    // Spaghetti Sauce ingredients
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817652"), IngredientId = 55, IsOptional = false, Quantity = 250.0m }, // Spaghetti
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817652"), IngredientId = 8, IsOptional = false, Quantity = 40.0m }, // Tomatoes
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817652"), IngredientId = 6, IsOptional = false, Quantity = 80.0m }, // Chicken Breast
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817652"), IngredientId = 30, IsOptional = false, Quantity = 8.0m }, // Herbs
                    new ItemIngredient { ItemId = Guid.Parse("ff0db05c-ffff-4044-ad7e-28f987817652"), IngredientId = 11, IsOptional = false, Quantity = 10.0m }, // Olive Oil

                    // Barbecue Corn ingredients
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817653"), IngredientId = 56, IsOptional = false, Quantity = 250.0m }, // Corn Kernels
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817653"), IngredientId = 45, IsOptional = false, Quantity = 25.0m }, // BBQ Sauce
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817653"), IngredientId = 31, IsOptional = false, Quantity = 5.0m }, // Spices
                    new ItemIngredient { ItemId = Guid.Parse("110db05c-1111-4044-ad7e-28f987817653"), IngredientId = 3, IsOptional = false, Quantity = 10.0m } // Butter
            };

            await context.ItemIngredients.AddRangeAsync(itemIngredients);
            await context.SaveChangesAsync();
        }
    }
} 