using System.Net;

using Todo.Domain.Common.Errors;

namespace Todo.Domain.Todos.Errors;

public sealed class TodoNotFoundException : DomainError, IError
{
    public TodoNotFoundException()
    {

    }
    public TodoNotFoundException(HttpStatusCode status)
    {
        Status = status;
    }
    public HttpStatusCode Status { get; init; } = HttpStatusCode.NoContent;
    public List<string> Errors { get; init; } = new() { """ "Todo/Terefa" não foi encontrado.""" };
}
