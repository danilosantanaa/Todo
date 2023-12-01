using Todo.Domain.Common.Models;

namespace Todo.Application.Common.Interfaces.Persistence;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, bool is_query_mode = false);
    Task<TEntity?> GetByIdAsync(ValueObject id, CancellationToken cancellationToken = default, bool is_query_mode = false);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);

    IQueryable<TEntity> GetQueryStmt();
}