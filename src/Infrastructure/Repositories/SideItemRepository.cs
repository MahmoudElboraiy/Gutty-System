using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SideItemRepository : ISideItemRepository
{
    private readonly ApplicationDbContext _context;

    public SideItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<SideItem>> GetSideItemsAsync()
    {
        return _context.SideItems.ToListAsync();
    }

    public Task<SideItem> GetSideItemByIdAsync(Guid id)
    {
        return _context.SideItems.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task CreateSideItemAsync(SideItem sideItem)
    {
        _context.SideItems.Add(sideItem);
        
        return Task.CompletedTask;
    }

    public Task UpdateSideItemAsync(SideItem sideItem)
    {
        _context.SideItems.Update(sideItem);
        
        return Task.CompletedTask;
    }

    public Task DeleteSideItemAsync(SideItem sideItem)
    {
        _context.SideItems.Remove(sideItem);
        
        return Task.CompletedTask;
    }

    public IQueryable GetAll()
    {
        return _context.SideItems;
    }
}