using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Todo.Infrastructure.Persistence.Contexts;

public class TodoDatabaseContext : DbContext
{
    public TodoDatabaseContext(DbContextOptions<TodoDatabaseContext> context) : base(context) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}