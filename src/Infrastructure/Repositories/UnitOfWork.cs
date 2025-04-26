using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    private IRepository<Item, Guid> _itemRepository;
    private IRepository<Ingredient, int> _ingredientRepository;
    private IRepository<IngredientChange, int> _ingredientChangeRepository;
    private IRepository<Plan, Guid> _planRepository;
    private IRepository<Subscription, Guid> _subscriptionRepository;
    private IRepository<ReferralCode, int> _referralCodeRepository;
    private IRepository<UserPrefernce, int> _userPreferenceRepository;
    private IRepository<ItemIngredient, int> _itemIngredientRepository;
    private IRepository<ExtraItemOption, int> _extraItemOptionRepository;
    private IRepository<PaymentLog, Guid> _paymentLogRepository;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IRepository<Item, Guid> Items => 
        _itemRepository ??= new Repository<Item, Guid>(_context);

    public IRepository<Ingredient, int> Ingredients => 
        _ingredientRepository ??= new Repository<Ingredient, int>(_context);

    public IRepository<IngredientChange, int> IngredientChanges => 
        _ingredientChangeRepository ??= new Repository<IngredientChange, int>(_context);

    public IRepository<Plan, Guid> Plans => 
        _planRepository ??= new Repository<Plan, Guid>(_context);

    public IRepository<Subscription, Guid> Subscriptions => 
        _subscriptionRepository ??= new Repository<Subscription, Guid>(_context);

    public IRepository<ReferralCode, int> ReferralCodes => 
        _referralCodeRepository ??= new Repository<ReferralCode, int>(_context);

    public IRepository<UserPrefernce, int> UserPreferences => 
        _userPreferenceRepository ??= new Repository<UserPrefernce, int>(_context);

    public IRepository<ItemIngredient, int> ItemIngredients => 
        _itemIngredientRepository ??= new Repository<ItemIngredient, int>(_context);

    public IRepository<ExtraItemOption, int> ExtraItemOptions => 
        _extraItemOptionRepository ??= new Repository<ExtraItemOption, int>(_context);

    public IRepository<PaymentLog, Guid> PaymentLogs => 
        _paymentLogRepository ??= new Repository<PaymentLog, Guid>(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
