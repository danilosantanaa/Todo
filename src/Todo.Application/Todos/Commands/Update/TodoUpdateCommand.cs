using MediatR;

namespace Todo.Application.Todos.Commands.Update;

public record TodoUpdateCommand(
    Guid TodoId,
    string Descricao,
    string Tipo,
    string RepeticaoTipo,
    DateTime DataConclusao,
    DateTime DataHoraLembrar,
    List<TodoEtapaUpdateCommand> TodoEtapas
) : IRequest<Guid>;

public record TodoEtapaUpdateCommand(
    Guid TodoEtapaId,
    string Descricao,
    DateTime DataExpiracao
);