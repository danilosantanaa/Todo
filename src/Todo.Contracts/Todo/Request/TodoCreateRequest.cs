namespace Todo.Contracts.Todo.Request;

public record TodoCreateRequest(
    string Descricao,
    string Tipo,
    string RepeticaoTipo,
    DateTime DataConclusao,
    DateTime DataHoraLembrar,
    Guid MenuId,
    List<TodoEtapaCreateRequest>? TodoEtapas
);

public record TodoEtapaCreateRequest(
    string Descricao,
    DateTime DataExpiracao
);