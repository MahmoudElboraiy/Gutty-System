using System.Linq.Expressions;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IRepository<TEntity, TKey>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task<bool> ExistsAsync(TKey id);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
    IQueryable<TEntity> GetQueryable();
}
