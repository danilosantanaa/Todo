using Todo.Domain.Common.Models;
using Todo.Domain.Todos.ValueObjects;

using todoDomain = Todo.Domain.Todos;

namespace Todo.Application.Common.Interfaces.Persistence;

public interface ITodoRepository : IRepository<todoDomain.Todo>
{
    Task<List<todoDomain.Todo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<todoDomain.Todo?> GetByIdAsync(TodoId id, CancellationToken cancellationToken = default);
}