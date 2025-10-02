using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Seeding.PromoCode
{
    public static class SeedPromoCode
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.PromoCodes.AnyAsync())
                return;
            var promoCodes = new List<Domain.Models.Entities.PromoCode>
            {
                new Domain.Models.Entities.PromoCode
                {
                    Id = Guid.NewGuid(),
                    Code = "WELCOME10",
                    DiscountType= DiscountType.Percentage,
                    DiscountValue =10,
                    IsActive = true,
                    ExpiryDate = DateTime.UtcNow.AddMonths(3),
                    OwnerUserId = "4f4e45bc-5fc1-4b38-b7ea-a78f70a5a0d2"
                },
                new Domain.Models.Entities.PromoCode
                {
                    Id = Guid.NewGuid(),
                     Code = "WELCOME100",
                    DiscountType= DiscountType.Fixed,
                    DiscountValue =200,
                    IsActive = true,
                    ExpiryDate = DateTime.UtcNow.AddMonths(3),
                }
            };
            await context.PromoCodes.AddRangeAsync(promoCodes);
            await context.SaveChangesAsync();   
        }
    }
}
