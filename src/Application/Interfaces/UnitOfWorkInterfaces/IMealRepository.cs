using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IMealRepository
{
    Task AddAsync(LaunchMeal launchMeal);
    Task UpdateAsync(LaunchMeal launchMeal);
    Task DeleteAsync(LaunchMeal launchMeal);
    Task<LaunchMeal?> FindByIdAsync(Guid id);
    Task<List<LaunchMeal>> GetAllAsync();
    IQueryable GetAll();
}
