namespace Todo.Contracts.Todo.Request;

public record TodoUpdateRequest(
    string Descricao,
    string Tipo,
    string RepeticaoTipo,
    DateTime DataConclusao,
    DateTime DataHoraLembrar,
    List<TodoEtapaUpdateRequest>? TodoEtapas
);

public record TodoEtapaUpdateRequest(
    Guid TodoEtapaId,
    string Descricao,
    DateTime DataExpiracao
);