using Microsoft.EntityFrameworkCore;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Common.Models;
using Todo.Infrastructure.Persistence.Contexts;

namespace Todo.Infrastructure.Persistence.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly TodoDatabaseContext _context;
    private IQueryable<TEntity> _query_stmt { get; set; } = null!;

    protected Repository(TodoDatabaseContext context)
    {
        _context = context;
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, bool is_query_mode = false)
    {
        _query_stmt = _context.Set<TEntity>();
        if (is_query_mode) return null!;

        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(ValueObject id, CancellationToken cancellationToken = default, bool is_query_mode = false)
    {
        _query_stmt = _context.Set<TEntity>();
        if (is_query_mode) return null;

        return await _context.Set<TEntity>().FindAsync(id, cancellationToken);
    }

    public IQueryable<TEntity> GetQueryStmt()
    {
        return _query_stmt;
    }
}