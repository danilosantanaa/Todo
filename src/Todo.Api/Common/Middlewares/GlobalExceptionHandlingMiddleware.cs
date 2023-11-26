using System.Net;
using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

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
        _logger.LogError(e, e.Message);
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        ProblemDetails problem = new()
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "Ocorreu um erro enquanto processava suas informações."
        };

        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsJsonAsync(problem);
    }
}