using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Seeding.Foods;

public static class SeedSubCategories
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Subcategories.AnyAsync())
            return;

        var subCategories = new List<Subcategory>
        {
            new Subcategory { Name = "Chicken Menu", CategoryId = 2 ,ImageUrl = "https://ichef.bbci.co.uk/food/ic/food_16x9_1600/recipes/air_fryer_roast_chicken_27390_16x9.jpg",}, 
            new Subcategory { Name = "Chicken Twagen", CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1678684279246-96e6afb970f2?q=80&w=1223&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", },
            new Subcategory { Name = "Meat Menu", CategoryId = 2 , ImageUrl = "https://media.istockphoto.com/id/1545218890/photo/roast-pork-loin-mushrooms-in-sauce-and-fresh-vegetables-on-wooden-table.webp?s=2048x2048&w=is&k=20&c=xcAI2pptwsdSF0pd873nYwreqbandLre3HQmhzTO9h4=",},
            new Subcategory { Name = "Twagen Meat", CategoryId = 2,ImageUrl = "https://ichef.bbci.co.uk/food/ic/food_16x9_1600/recipes/air_fryer_roast_chicken_27390_16x9.jpg" },
            new Subcategory { Name = "Sea Food Menu", CategoryId = 2,ImageUrl = "https://ichef.bbci.co.uk/food/ic/food_16x9_1600/recipes/air_fryer_roast_chicken_27390_16x9.jpg" },
            new Subcategory { Name = "Pizaa Menu", CategoryId = 2,ImageUrl = "https://www.alisoneroman.com/content/images/size/w1200/format/avif/images-squarespace-cdn-com/content/v1/541b1515e4b0a990b33a796e/1634744280316-0C8HLYGBQ5D2F7PSHRTT/2018_09_23_chicken_0383_2.jpg", },
            new Subcategory { Name = "Hawawshi Menu", CategoryId = 2,ImageUrl = "https://www.alisoneroman.com/content/images/size/w1200/format/avif/images-squarespace-cdn-com/content/v1/541b1515e4b0a990b33a796e/41af167f-941a-4d42-ad21-32a5db11a1a9/grilled_chicken_with_spicy_lime.jpg", },
            new Subcategory { Name = "Rice Menu", CategoryId = 3, ImageUrl = "https://www.lemonblossoms.com/wp-content/uploads/2019/11/How-To-Cook-White-Rice-S1-1.jpg", },
            new Subcategory { Name = "Pasta Menu", CategoryId = 3,ImageUrl = "https://www.lemonblossoms.com/wp-content/uploads/2019/11/How-To-Cook-White-Rice-S1-1.jpg", }, 
            new Subcategory { Name = "Protein Salads Menu", CategoryId = 1 ,ImageUrl = "https://images.services.kitchenstories.io/aJycJxbZPvDuWtlILdg7WiJfomY=/1920x0/filters:quality(85)/images.kitchenstories.io/communityImages/f4604e05f6a9eaca99afddd69e849005_c02485d4-0841-4de6-b152-69deb38693f2.jpg",},
            new Subcategory { Name = "Rolls Menu", CategoryId = 1,ImageUrl = "https://images.services.kitchenstories.io/aJycJxbZPvDuWtlILdg7WiJfomY=/1920x0/filters:quality(85)/images.kitchenstories.io/communityImages/f4604e05f6a9eaca99afddd69e849005_c02485d4-0841-4de6-b152-69deb38693f2.jpg" },
            new Subcategory { Name = "Toast Menu", CategoryId = 1,ImageUrl = "https://images.services.kitchenstories.io/aJycJxbZPvDuWtlILdg7WiJfomY=/1920x0/filters:quality(85)/images.kitchenstories.io/communityImages/f4604e05f6a9eaca99afddd69e849005_c02485d4-0841-4de6-b152-69deb38693f2.jpg" },
            new Subcategory { Name = "Pizaa Menu", CategoryId = 1,ImageUrl = "https://images.services.kitchenstories.io/aJycJxbZPvDuWtlILdg7WiJfomY=/1920x0/filters:quality(85)/images.kitchenstories.io/communityImages/f4604e05f6a9eaca99afddd69e849005_c02485d4-0841-4de6-b152-69deb38693f2.jpg" },
            new Subcategory { Name = "Eggs Menu", CategoryId = 1,ImageUrl = "https://images.services.kitchenstories.io/aJycJxbZPvDuWtlILdg7WiJfomY=/1920x0/filters:quality(85)/images.kitchenstories.io/communityImages/f4604e05f6a9eaca99afddd69e849005_c02485d4-0841-4de6-b152-69deb38693f2.jpg" },
            new Subcategory { Name = "Sugar Free Menu", CategoryId = 1,ImageUrl = "https://images.services.kitchenstories.io/aJycJxbZPvDuWtlILdg7WiJfomY=/1920x0/filters:quality(85)/images.kitchenstories.io/communityImages/f4604e05f6a9eaca99afddd69e849005_c02485d4-0841-4de6-b152-69deb38693f2.jpg" }
        };

        await context.Subcategories.AddRangeAsync(subCategories);
        await context.SaveChangesAsync();
    }
}
