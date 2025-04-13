using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IMainItemRepository
{
    Task<List<MainItem>> GetAllMainItemsAsync();
    Task<MainItem> GetMainItemByIdAsync(Guid id);
    Task CreateMainItemAsync(MainItem mainItem);
    Task UpdateMainItemAsync(MainItem mainItem);
    Task DeleteMainItemAsync(Guid id);
}