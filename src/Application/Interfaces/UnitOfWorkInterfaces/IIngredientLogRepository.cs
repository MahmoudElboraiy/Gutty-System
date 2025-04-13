using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IIngredientLogRepository
{
    Task AddAsync(IngredientLog log);
    IQueryable <IngredientLog> GetAll();
}