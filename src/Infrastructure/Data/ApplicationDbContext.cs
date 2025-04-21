using Domain.Models.Entities;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<LaunchMeal> Meals { get; set; }
    public DbSet<PaymentLog> PaymentLogs { get; set; }
    public DbSet<IngredientLog> IngredientLogs { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<PromoCode> PromoCodes { get; set; }
    public DbSet<UserPrefernce> UserPrefernces { get; set; }
    public DbSet<IngredientStock> IngredientStocks { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
}
