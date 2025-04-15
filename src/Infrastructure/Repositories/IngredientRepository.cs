using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class IngredientRepository :  IIngredientRepository
{
    private readonly ApplicationDbContext _context;
    
    public IngredientRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Ingredient ingredient)
    {
        await _context.Ingredients.AddAsync(ingredient);
    }

    public Task UpdateAsync(Ingredient ingredient)
    {
        _context.Ingredients.Update(ingredient);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Ingredient ingredient)
    {
        _context.Ingredients.Remove(ingredient);
        
        return Task.CompletedTask;
    }

    public Task<Ingredient?> GetAsync(int id)
    {
        return _context.Ingredients.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Ingredient>> GetAllAsync()
    {
        return _context.Ingredients.ToListAsync();
    }

    public Task<List<Ingredient>> GetAllWithoutTrackingAsync()
    {
        return _context.Ingredients.AsNoTracking().ToListAsync();
    }

    public IQueryable<Ingredient> GetAllQueryable()
    {
        return _context.Ingredients;
    }
}