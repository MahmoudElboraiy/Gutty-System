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
    public DbSet<PaymentLog> PaymentLogs { get; set; }
    public DbSet<ReferralCode> ReferralCodes { get; set; }
    public DbSet<UserPrefernce> UserPrefernces { get; set; }
    public DbSet<SubscriptionCategory> SubscriptionCategories { get; set; }
}
