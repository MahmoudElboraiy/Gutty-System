using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class IngredientLogRepository : IIngredientLogRepository
{
    private readonly ApplicationDbContext _context;

    public IngredientLogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(IngredientLog log)
    {
        _context.IngredientLogs.Add(log);
        
        return Task.CompletedTask;
    }

    public IQueryable<IngredientLog> GetAll()
    {
        return _context.IngredientLogs;
    }
}