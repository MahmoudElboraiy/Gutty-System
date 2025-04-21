using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class IngredientStockRepository : IIngredientStockRepository
{
    private readonly ApplicationDbContext _context;

    public IngredientStockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(IngredientStock stock)
    {
        _context.IngredientStocks.Add(stock);

        return Task.CompletedTask;
    }

    public Task<IngredientStock> GetAsync(int stockId)
    {
        return _context.IngredientStocks.FirstOrDefaultAsync(x => x.Id == stockId);
    }

    public Task UpdateAsync(IngredientStock stock)
    {
        _context.IngredientStocks.Update(stock);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(IngredientStock stock)
    {
        _context.IngredientStocks.Remove(stock);

        return Task.CompletedTask;
    }

    public Task<List<IngredientStock>> GetAllAsync()
    {
        return _context.IngredientStocks.ToListAsync();
    }

    public IQueryable GetQueryable()
    {
        return _context.IngredientStocks;
    }
}
