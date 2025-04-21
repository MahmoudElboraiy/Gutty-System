using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IItemRepository
{
    Task<List<Item>> GetAllMainItemsAsync();
    Task<Item?> GetItemByIdAsync(Guid id);
    Task CreateMainItemAsync(Item item);
    Task UpdateMainItemAsync(Item item);
    Task DeleteMainItemAsync(Guid id);
    IQueryable<Item> GetQueryable();
}
