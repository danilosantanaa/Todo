using MediatR;

namespace Todo.Application.Todos.Commands.Create;

public record TodoCreateCommand(
    string Descricao,
    string Tipo,
    string RepeticaoTipo,
    DateTime DataConclusao,
    DateTime DataHoraLembrar,
    Guid MenuId,
    List<TodoEtapaCreateCommand> TodoEtapas
) : IRequest<Guid>;


public record TodoEtapaCreateCommand(
    string Descricao,
    DateTime DataExpiracao
);