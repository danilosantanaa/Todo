using Microsoft.Extensions.DependencyInjection;

namespace Todo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        return service;
    }
}