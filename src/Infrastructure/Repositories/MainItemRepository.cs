using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MainItemRepository : IMainItemRepository
{
    private readonly ApplicationDbContext _context;

    public MainItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<MainItem>> GetAllMainItemsAsync()
    {
        return _context.MainItems.ToListAsync();
    }

    public Task<MainItem> GetMainItemByIdAsync(Guid id)
    {
        return _context.MainItems.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task CreateMainItemAsync(MainItem mainItem)
    {
        _context.MainItems.Add(mainItem);
        
        return Task.CompletedTask;
    }

    public Task UpdateMainItemAsync(MainItem mainItem)
    {
        _context.MainItems.Update(mainItem);
        
        return Task.CompletedTask;
    }

    public Task DeleteMainItemAsync(Guid id)
    {
        var mainItem = _context.MainItems.FirstOrDefault(x => x.Id == id);

        _context.MainItems.Remove(mainItem);
        
        return Task.CompletedTask;
    }
}