using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Todos.Entities;
using Todo.Infrastructure.Persistence.Contexts;

namespace Todo.Infrastructure.Persistence.Repositories;

public class TodoEtapaRepository : Repository<TodoEtapa>, ITodoEtapaRepository
{
    public TodoEtapaRepository(TodoDatabaseContext context) : base(context)
    {
    }
}