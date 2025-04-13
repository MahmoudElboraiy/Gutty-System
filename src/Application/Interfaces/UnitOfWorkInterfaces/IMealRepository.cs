using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IMealRepository
{
    Task AddAsync(Meal meal);
    Task UpdateAsync(Meal meal);
    Task DeleteAsync(Meal meal);
    Task<Meal?> FindByIdAsync(Guid id);
    Task<List<Meal>> GetAllAsync();
    IQueryable GetAll();
}