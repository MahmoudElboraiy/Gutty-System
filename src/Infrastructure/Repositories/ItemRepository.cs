using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _context;

    public ItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Item>> GetAllMainItemsAsync()
    {
        return _context.Items.ToListAsync();
    }

    public Task<Item?> GetItemByIdAsync(Guid id)
    {
        return _context.Items.Include(i=>i.RecipeIngredients).FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task CreateMainItemAsync(Item item)
    {
        _context.Items.Add(item);
        
        return Task.CompletedTask;
    }

    public Task UpdateMainItemAsync(Item item)
    {
        _context.Items.Update(item);
        
        return Task.CompletedTask;
    }

    public Task DeleteMainItemAsync(Guid id)
    {
        var mainItem = _context.Items.FirstOrDefault(x => x.Id == id);

        _context.Items.Remove(mainItem);
        
        return Task.CompletedTask;
    }

    public IQueryable<Item> GetQueryable()
    {
        return _context.Items;
    }
}