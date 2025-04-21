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

    public Task AddAsync(LaunchMeal launchMeal)
    {
        _context.Meals.Add(launchMeal);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(LaunchMeal launchMeal)
    {
        _context.Meals.Update(launchMeal);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(LaunchMeal launchMeal)
    {
        _context.Meals.Remove(launchMeal);

        return Task.CompletedTask;
    }

    public Task<LaunchMeal?> FindByIdAsync(Guid id)
    {
        return _context.Meals.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<LaunchMeal>> GetAllAsync()
    {
        return _context.Meals.ToListAsync();
    }

    public IQueryable GetAll()
    {
        return _context.Meals;
    }
}
