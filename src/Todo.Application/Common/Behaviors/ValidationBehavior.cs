using System.ComponentModel.DataAnnotations;

using FluentValidation;

using MediatR;

using Todo.Application.Common.Errors;

namespace Todo.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context)));

        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(ValidationResult => ValidationResult.Errors)
            .Select(validationFailures => new Error(validationFailures.PropertyName, validationFailures.ErrorMessage))
            .ToList();

        if (errors.Any())
        {
            throw new ValidationError(errors);
        }

        return await next();
    }
}
