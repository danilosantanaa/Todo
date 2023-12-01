using MediatR;

using TodoDomain = Todo.Domain.Todos;

namespace Todo.Application.Todos.Queries.GetAll;

public record TodoGetAllQuery() : IRequest<List<TodoDomain.Todo>>;