using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IUserPrefernceRepository
{
    Task AddAsync(UserPrefernce userPrefernce);
    Task<UserPrefernce> GetAsync(int id);
    Task<UserPrefernce> GetAsync(string userId);
    Task UpdateAsync(UserPrefernce userPrefernce);
    Task DeleteAsync(UserPrefernce userPrefernce);
    Task DeleteAsync(int id);
    Task DeleteAsync(string userId);
    IQueryable GetAll();
}
