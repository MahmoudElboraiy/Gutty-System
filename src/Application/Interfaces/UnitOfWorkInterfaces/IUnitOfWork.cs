using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IUnitOfWork
{
    IRepository<Plan, Guid> Plans { get; }
    IRepository<Subscription, Guid> Subscriptions { get; }
    IRepository<ReferralCode, int> ReferralCodes { get; }
    IRepository<UserPrefernce, int> UserPreferences { get; }

    IRepository<PaymentLog, Guid> PaymentLogs { get; }
    IRepository<ShippingAddress, int> ShippingAddresses { get; }
    IRepository<PromoCode,Guid>  PromoCodes { get; }
    IRepository<PromoCodeUsage ,Guid> PromoCodeUsages { get; }

    // Specialized repositories
    // IItemRepository ItemsSpecialized { get; }
    // IPlanRepository PlansSpecialized { get; }

    Task<int> CompleteAsync();
}
