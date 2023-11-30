using System.Net;

using Todo.Domain.Common.Errors;

namespace Todo.Domain.Menus.Errors;

public class MenuNotFoundException : DomainError, IError
{
    public MenuNotFoundException() { }

    public MenuNotFoundException(HttpStatusCode status)
    {
        Status = status;
    }
    public HttpStatusCode Status { get; init; } = HttpStatusCode.NoContent;
    public List<string> Errors { get; init; } = new List<string>() { "Menu não foi encontrado!" };
}