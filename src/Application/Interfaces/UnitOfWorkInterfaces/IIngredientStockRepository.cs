using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IIngredientStockRepository
{
    Task AddAsync(IngredientStock stock);
    Task<IngredientStock> GetAsync(int stockId);
    Task UpdateAsync(IngredientStock stock);
    Task DeleteAsync(IngredientStock stock);
    Task<List<IngredientStock>> GetAllAsync();
    IQueryable GetQueryable();
}
