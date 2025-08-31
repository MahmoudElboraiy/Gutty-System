using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IRepository<Meal, int> _mealRepository;
    private IRepository<Subcategory,int> _subcategoryRepository;
    private IRepository<Category, int> _categoryRepository;
    private IRepository<Plan, Guid> _planRepository;
    private IRepository<Subscription, Guid> _subscriptionRepository;
    private IRepository<ReferralCode, int> _referralCodeRepository;
    private IRepository<UserPrefernce, int> _userPreferenceRepository;
    private IRepository<PaymentLog, Guid> _paymentLogRepository;
    private IRepository<ShippingAddress, int> _shippingAddressRepository;
    private IRepository<PromoCode, Guid> _promoCodeRepository;
    private IRepository<PromoCodeUsage, Guid> _promoCodeUsageRepository;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }



    public IRepository<Plan, Guid> Plans =>
        _planRepository ??= new Repository<Plan, Guid>(_context);

    public IRepository<Subscription, Guid> Subscriptions =>
        _subscriptionRepository ??= new Repository<Subscription, Guid>(_context);

    public IRepository<ReferralCode, int> ReferralCodes =>
        _referralCodeRepository ??= new Repository<ReferralCode, int>(_context);

    public IRepository<UserPrefernce, int> UserPreferences =>
        _userPreferenceRepository ??= new Repository<UserPrefernce, int>(_context);


    public IRepository<PaymentLog, Guid> PaymentLogs =>
        _paymentLogRepository ??= new Repository<PaymentLog, Guid>(_context);

    public IRepository<ShippingAddress, int> ShippingAddresses =>
        _shippingAddressRepository ??= new Repository<ShippingAddress, int>(_context);
    public IRepository<PromoCode, Guid> PromoCodes =>
        _promoCodeRepository ??= new Repository<PromoCode, Guid>(_context);
    public IRepository<PromoCodeUsage, Guid> PromoCodeUsages =>
        _promoCodeUsageRepository ??= new Repository<PromoCodeUsage, Guid>(_context);
    public IRepository<Category, int> Categories =>
        _categoryRepository ??= new Repository<Category, int>(_context);
    public IRepository<Subcategory, int> SubCategories =>
        _subcategoryRepository ??= new Repository<Subcategory, int>(_context);
    public IRepository<Meal, int> Meals =>
        _mealRepository ??= new Repository<Meal, int>(_context);
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
