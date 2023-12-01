using MediatR;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Todos.Errors;
using Todo.Domain.Todos.ValueObjects;

using TodoDomain = Todo.Domain.Todos;

namespace Todo.Application.Todos.Queries.GetById;

public sealed class TodoGetByIdQueryHandler : IRequestHandler<TodoGetByIdQuery, TodoDomain.Todo>
{
    private readonly IUnitOfWork _unitOfWork;

    public TodoGetByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TodoDomain.Todo> Handle(TodoGetByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await _unitOfWork.TodoRepository.GetByIdAsync(TodoId.Create(request.todoId));
        if (todo is null)
        {
            throw new TodoNotFoundException();
        }

        return todo;

    }
}
