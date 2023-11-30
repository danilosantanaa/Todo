using System.Net;

using Todo.Domain.Common.Errors;

namespace Todo.Domain.Todos.Errors;

public sealed class TodoEtapaNaoPodeSerAdicionadoException : DomainError, IError
{
    public HttpStatusCode Status { get; init; } = HttpStatusCode.BadRequest;
    public List<string> Errors { get; init; } = new() { "Para Terafa Geral, não é permitido colocar Etapas." };
}