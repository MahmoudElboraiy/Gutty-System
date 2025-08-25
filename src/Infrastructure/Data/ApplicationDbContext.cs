using Domain.Models.Entities;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<PromoCode> PromoCodes { get; set; }
    public DbSet<PromoCodeUsage> PromoCodeUsages { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<PlanCategory> PlanCategories { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<IngredientChange> IngredientChanges { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<PaymentLog> PaymentLogs { get; set; }
    public DbSet<ReferralCode> ReferralCodes { get; set; }
    public DbSet<UserPrefernce> UserPrefernces { get; set; }
    public DbSet<ExtraItemOption> ExtraItemOptions { get; set; }
    public DbSet<ItemIngredient> ItemIngredients { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<SubscriptionCategory> SubscriptionCategories { get; set; }
}
