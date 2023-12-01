using System.Net;

using MediatR;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Common.Services;
using Todo.Domain.Todos.Enums;
using Todo.Domain.Todos.Errors;
using Todo.Domain.Todos.ValueObjects;

namespace Todo.Application.Todos.Commands.Update;

public sealed class TodoUpdateCommandHandler : IRequestHandler<TodoUpdateCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public TodoUpdateCommandHandler(IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> Handle(TodoUpdateCommand request, CancellationToken cancellationToken)
    {
        var todoId = TodoId.Create(request.TodoId);
        var todo = await _unitOfWork.TodoRepository.GetByIdAsync(todoId);
        if (todo is null)
        {
            throw new TodoNotFoundException(HttpStatusCode.BadRequest);
        }

        TodoTipo tipo = Enum.Parse<TodoTipo>(request.Tipo);
        TodoRepeticaoTipo repeticaoTipo = Enum.Parse<TodoRepeticaoTipo>(request.RepeticaoTipo);

        todo.Update(request.Descricao, tipo, repeticaoTipo);

        if (request.TodoEtapas.Any())
        {
            RemoveEtapas(request, todo);
            AddOrUpdateEtapas(request, todo);
        }

        _unitOfWork.TodoRepository.Update(todo);
        await _unitOfWork.SaveChangesAsync();

        return todo.Id.Value;
    }

    private void AddOrUpdateEtapas(TodoUpdateCommand request, Domain.Todos.Todo? todo)
    {
        // Adiciona/Atualizar Etaoa
        foreach (var etapa in request.TodoEtapas)
        {
            TodoEtapaId todoEtapaId = TodoEtapaId.Create(etapa.TodoEtapaId);

            if (todo!.IsEtapaExists(todoEtapaId))
            {
                var result = todo.UpdateEtapa(todoEtapaId, etapa.Descricao, _dateTimeProvider, etapa.DataExpiracao);
                _unitOfWork.TodoEtapaRepository.Update(result);
            }
            else
            {
                var result = todo.AddEtapa(etapa.Descricao, _dateTimeProvider, etapa.DataExpiracao);
                _unitOfWork.TodoEtapaRepository.Add(result);
            }
        }
    }

    private void RemoveEtapas(TodoUpdateCommand request, Domain.Todos.Todo? todo)
    {
        // Remover Etapas
        var etapasDeletadas =
            todo?.TodoEtapas
                .Where(x =>
                    !request.TodoEtapas
                    .Select(y => y.TodoEtapaId)
                    .Contains(x.Id.Value)
                )
                .ToList();

        foreach (var etapa in etapasDeletadas!)
        {
            if (request.TodoEtapas.Find(x => x.TodoEtapaId == etapa.Id.Value) is null)
            {
                _unitOfWork.TodoEtapaRepository.Delete(etapa);
            }
        }
    }
}
