using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IPlanRepository
{
    Task AddAsync(Plan plan);
    Task<Plan> GetAsync(Guid id);
    Task<List<Plan>> GetAllAsync();
    Task UpdateAsync(Plan plan);
    Task DeleteAsync(Plan plan);
    IQueryable GetAll();
}
