using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface ISubscriptionRepository
{
    Task<Subscription> GetSubscriptionByUserIdAsync(string userId);
    Task<Subscription> GetSubscriptionByIdAsync(Guid id);
    Task CreateSubscriptionAsync(Subscription subscription);
    Task UpdateSubscriptionAsync(Subscription subscription);
    Task DeleteSubscriptionAsync(Subscription subscription);

    IQueryable GetAll();
}