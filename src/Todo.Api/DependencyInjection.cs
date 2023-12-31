using Todo.Api.Common.Mappings;
using Todo.Api.Common.Middlewares;

namespace Todo.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {

        services.AddEndpointsApiExplorer();
        services.AddLogging();
        services.AddSwaggerGen();
        services.AddControllers();
        services.AddMappings();

        services.AddTransient<GlobalExceptionHandlingMiddleware>();

        return services;
    }
}