namespace Todo.Contracts.Todo.Response;

public record TodoResponse(
    Guid Id,
    string Descricao,
    string Tipo,
    string RepeticaoTipo,
    DateTime DataConclusao,
    DateTime DataHoraLembrar,
    Guid MenuId,
    List<TodoEtapaResponse>? TodoEtapas
);

public record TodoEtapaResponse(
    Guid Id,
    string Descricao,
    DateTime DataExpiracao
);