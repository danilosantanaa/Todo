using System.Net;

using Todo.Domain.Common.Errors;

namespace Todo.Domain.Todos.Errors;

public sealed class TodoEtapaDataExpiracaoInvalidoException : DomainError, IError
{
    public HttpStatusCode Status { get; init; } = HttpStatusCode.BadGateway;
    public List<string> Errors { get; init; } = new() { """A "Data de Expiracao" não pode ser menor que a "Data Atual".""" };
}
