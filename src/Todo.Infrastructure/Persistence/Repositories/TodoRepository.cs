using Todo.Application.Common.Interfaces.Persistence;
using Todo.Infrastructure.Persistence.Contexts;

using todoDomain = Todo.Domain.Todos;

namespace Todo.Infrastructure.Persistence.Repositories;

public class TodoRepository : Repository<todoDomain.Todo>, ITodoRepository
{
    public TodoRepository(TodoDatabaseContext context) : base(context)
    {
    }
}