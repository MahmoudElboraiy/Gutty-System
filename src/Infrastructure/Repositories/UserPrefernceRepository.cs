using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserPrefernceRepository : IUserPrefernceRepository
{
    private readonly ApplicationDbContext _context;

    public UserPrefernceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(UserPrefernce userPrefernce)
    {
        _context.UserPrefernces.Add(userPrefernce);
        
        return Task.CompletedTask;
    }

    public Task<UserPrefernce> GetAsync(int id)
    {
        return _context.UserPrefernces.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<UserPrefernce> GetAsync(string userId)
    {
        return _context.UserPrefernces.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public Task UpdateAsync(UserPrefernce userPrefernce)
    {
        _context.UserPrefernces.Update(userPrefernce);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(UserPrefernce userPrefernce)
    {
        _context.UserPrefernces.Remove(userPrefernce);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var userPrefernce = _context.UserPrefernces.FirstOrDefault(x => x.Id == id);

        _context.UserPrefernces.Remove(userPrefernce);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string userId)
    {
        var userPrefernce = _context.UserPrefernces.FirstOrDefault(x => x.UserId == userId);

        _context.UserPrefernces.Remove(userPrefernce);
        
        return Task.CompletedTask;
    }

    public IQueryable GetAll()
    {
        return _context.UserPrefernces;
    }
}
