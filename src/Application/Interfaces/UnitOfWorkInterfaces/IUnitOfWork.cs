using Domain.Models.Entities;
namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IUnitOfWork
{
    IRepository<Ingredient, int> Ingredients { get; }
    IRepository<Purchases, int> Purchases { get; }
    IRepository<Sales, int> Sales { get; }
    IRepository<Order,int> Orders { get; }
    IRepository<OrderMeal, int> OrderMeals { get; }
    IRepository<Meal, int> Meals { get; }
    IRepository<Category, int> Categories { get; }
    IRepository<Subcategory, int> SubCategories { get; }
    IRepository<Plan, Guid> Plans { get; }
    IRepository<Subscription, Guid> Subscriptions { get; }
    IRepository<ReferralCode, int> ReferralCodes { get; }
    IRepository<UserPrefernce, int> UserPreferences { get; }

    IRepository<PaymentLog, Guid> PaymentLogs { get; }
    IRepository<ShippingAddress, int> ShippingAddresses { get; }
    IRepository<PromoCode,Guid>  PromoCodes { get; }
    IRepository<PromoCodeUsage ,Guid> PromoCodeUsages { get; }
    IRepository<Configurations, int> Configurations { get; }

    // Specialized repositories
    // IItemRepository ItemsSpecialized { get; }
    // IPlanRepository PlansSpecialized { get; }

    Task<int> CompleteAsync();
}
