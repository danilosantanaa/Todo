using Microsoft.EntityFrameworkCore;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Common.Models;
using Todo.Domain.Todos.ValueObjects;
using Todo.Infrastructure.Persistence.Contexts;

using todoDomain = Todo.Domain.Todos;

namespace Todo.Infrastructure.Persistence.Repositories;

public class TodoRepository : Repository<todoDomain.Todo>, ITodoRepository
{
    public TodoRepository(TodoDatabaseContext context) : base(context)
    {
    }

    public async Task<List<todoDomain.Todo>> GetAllAsync(CancellationToken cancellationToken)
    {
        await base.GetAllAsync(is_query_mode: true);
        var query = GetQueryStmt();

        return await query.Include(x => x.TodoEtapas).ToListAsync();
    }

    public async Task<todoDomain.Todo?> GetByIdAsync(TodoId id, CancellationToken cancellationToken)
    {
        await base.GetByIdAsync(id, is_query_mode: true);
        var query = GetQueryStmt();

        return await query.Include(x => x.TodoEtapas).Where(t => t.Id == id).FirstOrDefaultAsync();
    }
}