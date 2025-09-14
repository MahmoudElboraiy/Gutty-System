using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Seeding.Foods
{
    public static class SeedIngredient
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Ingredients.AnyAsync())
                return;

            var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Flour", NameAr = "دقيق", Description = "All-purpose wheat flour", StockQuantity = 100.0m },
                new Ingredient { Name = "Sugar", NameAr = "سكر", Description = "White granulated sugar", StockQuantity = 50.0m },
                new Ingredient { Name = "Butter", NameAr = "زبدة", Description = "Unsalted butter", StockQuantity = 30.0m },
                new Ingredient { Name = "Eggs", NameAr = "بيض", Description = "Fresh chicken eggs", StockQuantity = 60.0m },
                new Ingredient { Name = "Milk", NameAr = "لبن", Description = "Whole milk", StockQuantity = 40.0m },
                new Ingredient { Name = "Chicken Breast", NameAr = "صدور الدجاج", Description = "Fresh boneless chicken breast", StockQuantity = 25.0m },
                new Ingredient { Name = "Rice", NameAr = "رز", Description = "White long-grain rice", StockQuantity = 80.0m },
                new Ingredient { Name = "Tomatoes", NameAr = "طماطم", Description = "Fresh ripe tomatoes", StockQuantity = 45.0m },
                new Ingredient { Name = "Lettuce", NameAr = "خس", Description = "Fresh iceberg lettuce", StockQuantity = 20.0m },
                new Ingredient { Name = "Cheese", NameAr = "جبن", Description = "Shredded mozzarella cheese", StockQuantity = 35.0m },
                new Ingredient { Name = "Olive Oil", NameAr = "زيت الزيتون", Description = "Extra virgin olive oil", StockQuantity = 15.0m },
                new Ingredient { Name = "Onions", NameAr = "بصل", Description = "Fresh yellow onions", StockQuantity = 30.0m },
                new Ingredient { Name = "Garlic", NameAr = "ثوم", Description = "Fresh garlic cloves", StockQuantity = 10.0m },
                new Ingredient { Name = "Bell Peppers", NameAr = "فلفل حلو", Description = "Mixed colored bell peppers", StockQuantity = 25.0m },
                new Ingredient { Name = "Cucumber", NameAr = "خيار", Description = "Fresh cucumber", StockQuantity = 20.0m },
                new Ingredient { Name = "Carrots", NameAr = "جزر", Description = "Fresh carrots", StockQuantity = 30.0m },
                new Ingredient { Name = "Potatoes", NameAr = "بطاطس", Description = "Fresh potatoes", StockQuantity = 40.0m },
                new Ingredient { Name = "Eggplant", NameAr = "باذنجان", Description = "Fresh eggplant", StockQuantity = 15.0m },
                new Ingredient { Name = "Zucchini", NameAr = "كوسة", Description = "Fresh zucchini", StockQuantity = 20.0m },
                new Ingredient { Name = "Spinach", NameAr = "سبانخ", Description = "Fresh spinach leaves", StockQuantity = 15.0m },
                new Ingredient { Name = "Mushrooms", NameAr = "فطر", Description = "Fresh button mushrooms", StockQuantity = 18.0m },
                new Ingredient { Name = "Quinoa", NameAr = "كينوا", Description = "Organic quinoa grains", StockQuantity = 25.0m },
                new Ingredient { Name = "Chickpeas", NameAr = "حمص", Description = "Dried chickpeas", StockQuantity = 30.0m },
                new Ingredient { Name = "Salmon", NameAr = "سلمون", Description = "Fresh salmon fillet", StockQuantity = 12.0m },
                new Ingredient { Name = "Beef", NameAr = "لحم بقر", Description = "Lean beef strips", StockQuantity = 15.0m },
                new Ingredient { Name = "Turkey", NameAr = "ديك رومي", Description = "Ground turkey meat", StockQuantity = 10.0m },
                new Ingredient { Name = "Greek Yogurt", NameAr = "زبادي يوناني", Description = "Plain Greek yogurt", StockQuantity = 20.0m },
                new Ingredient { Name = "Berries", NameAr = "توت", Description = "Mixed fresh berries", StockQuantity = 12.0m },
                new Ingredient { Name = "Granola", NameAr = "جرانولا", Description = "Homemade granola", StockQuantity = 15.0m },
                new Ingredient { Name = "Herbs", NameAr = "أعشاب", Description = "Mixed fresh herbs", StockQuantity = 8.0m },
                new Ingredient { Name = "Spices", NameAr = "بهارات", Description = "Mixed cooking spices", StockQuantity = 12.0m },
                new Ingredient { Name = "Lemon", NameAr = "ليمون", Description = "Fresh lemons", StockQuantity = 18.0m },
                new Ingredient { Name = "Parsley", NameAr = "بقدونس", Description = "Fresh parsley", StockQuantity = 10.0m },
                new Ingredient { Name = "Cilantro", NameAr = "كزبرة", Description = "Fresh cilantro", StockQuantity = 8.0m },
                new Ingredient { Name = "Ginger", NameAr = "زنجبيل", Description = "Fresh ginger root", StockQuantity = 5.0m },
                // New ingredients for meat, seafood, pizza, and hawawshi items
                new Ingredient { Name = "Ground Beef", NameAr = "لحم بقر مفروم", Description = "Fresh ground beef", StockQuantity = 20.0m },
                new Ingredient { Name = "White Fish Fillet", NameAr = "فيليه سمك أبيض", Description = "Fresh white fish fillet", StockQuantity = 15.0m },
                new Ingredient { Name = "Shrimp", NameAr = "جمبري", Description = "Fresh shrimp", StockQuantity = 12.0m },
                new Ingredient { Name = "Tuna", NameAr = "تونة", Description = "Fresh tuna", StockQuantity = 10.0m },
                new Ingredient { Name = "Mozzarella Cheese", NameAr = "جبن موتزاريلا", Description = "Fresh mozzarella cheese", StockQuantity = 25.0m },
                new Ingredient { Name = "Pizza Dough", NameAr = "عجينة بيتزا", Description = "Whole grain pizza dough", StockQuantity = 30.0m },
                new Ingredient { Name = "Tahini", NameAr = "طحينة", Description = "Sesame tahini paste", StockQuantity = 15.0m },
                new Ingredient { Name = "Bechamel Sauce", NameAr = "صلصة البشاميل", Description = "Creamy bechamel sauce", StockQuantity = 20.0m },
                new Ingredient { Name = "Ranch Sauce", NameAr = "صلصة الرانش", Description = "Creamy ranch dressing", StockQuantity = 18.0m },
                new Ingredient { Name = "BBQ Sauce", NameAr = "صلصة الباربيكيو", Description = "Smoky BBQ sauce", StockQuantity = 15.0m },
                new Ingredient { Name = "Pita Bread", NameAr = "خبز البيتا", Description = "Fresh pita bread", StockQuantity = 40.0m },
                new Ingredient { Name = "Molokhia", NameAr = "ملوخية", Description = "Fresh molokhia leaves", StockQuantity = 10.0m },
                new Ingredient { Name = "Ground Chicken", NameAr = "دجاج مفروم", Description = "Fresh ground chicken", StockQuantity = 18.0m },
                // Additional ingredients for carb items
                new Ingredient { Name = "Basmati Rice", NameAr = "أرز بسمتي", Description = "Premium basmati rice", StockQuantity = 50.0m },
                new Ingredient { Name = "Mixed Vegetables", NameAr = "خضار مشكلة", Description = "Carrots, peas and mixed vegetables", StockQuantity = 25.0m },
                new Ingredient { Name = "Biryani Spices", NameAr = "بهارات البرياني", Description = "Traditional biryani spice mix", StockQuantity = 10.0m },
                new Ingredient { Name = "Pasta", NameAr = "مكرونة", Description = "Whole grain pasta", StockQuantity = 40.0m },
                new Ingredient { Name = "Fettuccine", NameAr = "فيتوتشيني", Description = "Fresh fettuccine pasta", StockQuantity = 30.0m },
                new Ingredient { Name = "Macaroni", NameAr = "مكرونة مخروطة", Description = "Classic macaroni pasta", StockQuantity = 35.0m },
                new Ingredient { Name = "Spaghetti", NameAr = "سباجيتي", Description = "Long spaghetti pasta", StockQuantity = 40.0m },
                new Ingredient { Name = "Corn Kernels", NameAr = "ذرة", Description = "Sweet corn kernels", StockQuantity = 20.0m },
                new Ingredient { Name = "White Sauce", NameAr = "صوص أبيض", Description = "Healthy white cream sauce", StockQuantity = 15.0m }
            };

            await context.Ingredients.AddRangeAsync(ingredients);
            await context.SaveChangesAsync();
        }
    }
}
