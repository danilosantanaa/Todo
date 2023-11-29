using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using Todo.Application.Common.Behaviors;

namespace Todo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        service.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return service;
    }
}