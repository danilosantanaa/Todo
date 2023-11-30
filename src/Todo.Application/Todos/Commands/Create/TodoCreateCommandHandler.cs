using MediatR;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Menus.Errors;
using Todo.Domain.Menus.ValueObjects;
using Todo.Domain.Todos.Entities;
using Todo.Domain.Todos.Enums;
using Todo.Domain.Todos.ValueObjects;

using TodoDomain = Todo.Domain.Todos;

namespace Todo.Application.Todos.Commands.Create;

public sealed class TodoCreateCommandHandler : IRequestHandler<TodoCreateCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public TodoCreateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(TodoCreateCommand request, CancellationToken cancellationToken)
    {
        var menuId = MenuId.Create(request.MenuId);
        var menu = await _unitOfWork.MenuRepository.GetByIdAsync(menuId);
        if (menu is null)
        {
            throw new MenuNotFoundException();
        }

        TodoTipo tipo = Enum.Parse<TodoTipo>(request.Tipo);
        TodoRepeticaoTipo repeticaoTipo = Enum.Parse<TodoRepeticaoTipo>(request.RepeticaoTipo);

        // TODO: Melhorar o Data Conclusão e Data Hora Lembrar
        TodoDomain.Todo todo = TodoDomain.Todo.Create(request.Descricao, tipo, repeticaoTipo, menuId);

        foreach (var todoEtapaRequest in request.TodoEtapas)
        {
            todo.AddEtapa(todoEtapaRequest.Descricao, todoEtapaRequest.DataExpiracao);
        }

        _unitOfWork.TodoRepository.Add(todo);
        await _unitOfWork.SaveChangesAsync();

        return todo.Id.Value;
    }
}