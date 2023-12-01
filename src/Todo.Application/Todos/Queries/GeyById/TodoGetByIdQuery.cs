using MediatR;

using TodoDomain = Todo.Domain.Todos;

namespace Todo.Application.Todos.Queries.GetById;

public record TodoGetByIdQuery(Guid todoId) : IRequest<TodoDomain.Todo>;