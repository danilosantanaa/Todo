using System.Net;

namespace Todo.Domain.Common.Errors;

public interface IError
{
    HttpStatusCode Status { get; init; }
    List<string> Errors { get; init; }
}