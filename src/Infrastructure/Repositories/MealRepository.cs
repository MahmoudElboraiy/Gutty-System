using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MealRepository : IMealRepository
{
    private readonly ApplicationDbContext _context;

    public MealRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(Meal meal)
    {
        _context.Meals.Add(meal);
        
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Meal meal)
    {
        _context.Meals.Update(meal);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Meal meal)
    {
        _context.Meals.Remove(meal);
        
        return Task.CompletedTask;
    }

    public Task<Meal?> FindByIdAsync(Guid id)
    {
        return _context.Meals.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Meal>> GetAllAsync()
    {
        return _context.Meals.ToListAsync();
    }

    public IQueryable GetAll()
    {
        return _context.Meals;
    }
}