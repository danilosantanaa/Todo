using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Npgsql;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Menus;
using Todo.Infrastructure.Persistence.Contexts;
using Todo.Infrastructure.Persistence.Repositories;
using Todo.Infrastructure.Persistences.Repositories;

namespace Todo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<TodoDatabaseContext>(context =>
            context.UseNpgsql(configuration.GetConnectionString("PostgresConnect"))
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}