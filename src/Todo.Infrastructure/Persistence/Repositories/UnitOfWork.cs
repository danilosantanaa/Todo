using Todo.Application.Common.Interfaces.Persistence;
using Todo.Infrastructure.Persistence.Contexts;

namespace Todo.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TodoDatabaseContext _context;

    public UnitOfWork(TodoDatabaseContext context)
    {
        _context = context;

        MenuRepository = new MenuRepository(_context);
        TodoRepository = new TodoRepository(_context);
    }

    public IMenuRepository MenuRepository { get; set; }
    public ITodoRepository TodoRepository { get; set; }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}