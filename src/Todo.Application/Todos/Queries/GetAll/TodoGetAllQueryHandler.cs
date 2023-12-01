using MediatR;

using Todo.Application.Common.Interfaces.Persistence;

using TodoDomain = Todo.Domain.Todos;

namespace Todo.Application.Todos.Queries.GetAll;

public sealed class TodoGetAllQueryHandler : IRequestHandler<TodoGetAllQuery, List<TodoDomain.Todo>>
{
    private readonly IUnitOfWork _unitOfWork;

    public TodoGetAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<TodoDomain.Todo>> Handle(TodoGetAllQuery request, CancellationToken cancellationToken)
    {
        var todos = await _unitOfWork.TodoRepository.GetAllAsync(cancellationToken);
        return todos;
    }
}
