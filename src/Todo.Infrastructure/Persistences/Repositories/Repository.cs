using Todo.Application.Common.Interfaces.Persistence;

namespace Todo.Infrastructure.Persistence.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{

    public void Add(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }
}