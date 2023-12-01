using System.Net;

using MediatR;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Todos.Errors;
using Todo.Domain.Todos.ValueObjects;

namespace Todo.Application.Todos.Commands.Delete;

public sealed class TodoDeleteCommandHandler : IRequestHandler<TodoDeleteCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public TodoDeleteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(TodoDeleteCommand request, CancellationToken cancellationToken)
    {
        TodoId todoId = TodoId.Create(request.todoId);

        var todo = await _unitOfWork.TodoRepository.GetByIdAsync(todoId);
        if (todo is null)
        {
            throw new TodoNotFoundException(HttpStatusCode.BadRequest);
        }

        _unitOfWork.TodoRepository.Delete(todo);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
