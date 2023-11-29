using System.Net;
using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Todo.Application.Common.Errors;
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

        ProblemDetails problem =
            e as ValidationError is not null ?
                ErrorValidations(context, e)
            : ErrorDomain(context, e);

        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsJsonAsync(problem);
    }

    private ProblemDetails ErrorValidations(HttpContext context, Exception e)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        ValidationError validationError = (ValidationError)e;

        ProblemDetails problem = new()
        {
            Status = (int)HttpStatusCode.BadRequest,
            Title = "Validations Errors"
        };

        Dictionary<string, List<string>> errorsDetails = new();
        foreach (var error in validationError.Errors)
        {
            if (errorsDetails.ContainsKey(error.PropertyName))
            {
                errorsDetails[error.PropertyName].Add(error.ErrorMessage);
            }
            else
            {
                errorsDetails.Add(error.PropertyName, new List<string>() { error.ErrorMessage });
            }
        }

        problem.Extensions.Add("Errors", errorsDetails);

        return problem;
    }

    private ProblemDetails ErrorDomain(HttpContext context, Exception e)
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

        return problem;
    }
}