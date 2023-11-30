namespace Todo.Contracts.Todo.Request;

public record TodoRequest(
    string Descricao,
    string Tipo,
    string RepeticaoTipo,
    DateTime DataConclusao,
    DateTime DataHoraLembrar,
    Guid MenuId,
    List<TodoEtapaRequest>? TodoEtapas
);

public record TodoEtapaRequest(
    string Descricao,
    DateTime DataExpiracao
);