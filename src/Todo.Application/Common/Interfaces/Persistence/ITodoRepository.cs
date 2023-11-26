using todoDomain = Todo.Domain.Todos;

namespace Todo.Application.Common.Interfaces.Persistence;

public interface ITodoRepository : IRepository<todoDomain.Todo>
{

}