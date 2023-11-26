namespace Todo.Application.Common.Interfaces.Persistence;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<bool> SaveChangeAsync(CancellationToken cancellationToken = default);
}