using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Subscription> GetSubscriptionByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Subscription> GetSubscriptionByIdAsync(Guid id)
    {
        return _context.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task CreateSubscriptionAsync(Subscription subscription)
    {
        _context.Subscriptions.Add(subscription);
        
        return Task.CompletedTask;
    }

    public Task UpdateSubscriptionAsync(Subscription subscription)
    {
        _context.Subscriptions.Update(subscription);
        
        return Task.CompletedTask;
    }

    public Task DeleteSubscriptionAsync(Subscription subscription)
    {
        _context.Subscriptions.Remove(subscription);
        
        return Task.CompletedTask;
    }

    public IQueryable GetAll()
    {
        return _context.Subscriptions;
    }
}