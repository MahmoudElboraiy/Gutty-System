using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IUnitOfWork
{
    IRepository<Item, Guid> Items { get; }
    IRepository<Ingredient, int> Ingredients { get; }
    IRepository<IngredientChange, int> IngredientChanges { get; }
    IRepository<Plan, Guid> Plans { get; }
    IRepository<Subscription, Guid> Subscriptions { get; }
    IRepository<ReferralCode, int> ReferralCodes { get; }
    IRepository<UserPrefernce, int> UserPreferences { get; }
    IRepository<ItemIngredient, int> ItemIngredients { get; }
    IRepository<ExtraItemOption, int> ExtraItemOptions { get; }
    IRepository<PaymentLog, Guid> PaymentLogs { get; }
        
    // Specialized repositories
    // IItemRepository ItemsSpecialized { get; }
    // IPlanRepository PlansSpecialized { get; }
        
    Task<int> CompleteAsync();
}
