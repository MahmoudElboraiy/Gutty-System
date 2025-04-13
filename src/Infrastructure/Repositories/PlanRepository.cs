using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PlanRepository : IPlanRepository
{
    private readonly ApplicationDbContext _context;

    public PlanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(Plan plan)
    {
        _context.Plans.Add(plan);
        
        return Task.CompletedTask;
    }

    public Task<Plan> GetAsync(Guid id)
    {
        return _context.Plans.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Plan>> GetAllAsync()
    {
        return _context.Plans.ToListAsync();
    }

    public Task UpdateAsync(Plan plan)
    {
        _context.Plans.Update(plan);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Plan plan)
    {
        _context.Plans.Remove(plan);
        
        return Task.CompletedTask;
    }

    public IQueryable GetAll()
    {
        return _context.Plans;
    }
}