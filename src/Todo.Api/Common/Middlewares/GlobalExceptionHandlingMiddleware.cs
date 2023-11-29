using System.Net;
using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

using Todo.Domain.Common.Errors;

namespace Todo.Api.Common.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await TreatError(context, e);
        }
    }

    private async Task TreatError(HttpContext context, Exception e)
    {
        (string title, HttpStatusCode status) errorException = e switch
        {
            IError error => (error.Errors[0], error.Status),
            _ => ("Ocorreu um error enquanto processava suas informações", HttpStatusCode.InternalServerError)
        };

        _logger.LogError(e, e.Message);
        context.Response.StatusCode = (int)errorException.status;

        ProblemDetails problem = new()
        {
            Status = (int)errorException.status,
            Title = errorException.title
        };

        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsJsonAsync(problem);
    }
}