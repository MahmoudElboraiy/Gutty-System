using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface ISideItemRepository
{
    Task<List<SideItem>> GetSideItemsAsync();
    Task<SideItem> GetSideItemByIdAsync(Guid id);
    Task CreateSideItemAsync(SideItem sideItem);
    Task UpdateSideItemAsync(SideItem sideItem);
    Task DeleteSideItemAsync(SideItem sideItem);
    IQueryable GetAll();
}