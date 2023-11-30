using System.Net;

using Todo.Domain.Common.Errors;

namespace Todo.Domain.Todos.Errors;

public sealed class TodoDataConclusaoMenorQueDateTimeAtual : DomainError, IError
{
    public HttpStatusCode Status { get; init; } = HttpStatusCode.BadRequest;
    public List<string> Errors { get; init; } = new() { $"""A "Data Hora Lembrar" não pode ser menor que "${DateTime.UtcNow}".""" };
}