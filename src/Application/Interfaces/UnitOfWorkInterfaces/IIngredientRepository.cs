using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IIngredientRepository
{
    Task AddAsync(Ingredient ingredient);
    Task UpdateAsync(Ingredient ingredient);
    Task DeleteAsync(Ingredient ingredient);
    Task<Ingredient?> GetAsync(int id);
    Task<Ingredient?> GetWithoutTrackingAsync(int id);
    Task<List<Ingredient>> GetAllAsync();
    Task<List<Ingredient>> GetAllWithoutTrackingAsync();
    IQueryable<Ingredient> GetAllQueryable();
}