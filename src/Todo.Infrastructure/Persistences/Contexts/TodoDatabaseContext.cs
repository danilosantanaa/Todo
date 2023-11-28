using Microsoft.EntityFrameworkCore;

using Todo.Domain.Menus;
using Todo.Domain.Todos.Entities;

using TodoDomain = Todo.Domain.Todos;

namespace Todo.Infrastructure.Persistence.Contexts;

public class TodoDatabaseContext : DbContext
{
    public TodoDatabaseContext(DbContextOptions<TodoDatabaseContext> context) : base(context) { }

    DbSet<Menu> Menus { get; set; }
    DbSet<TodoDomain.Todo> Todos { get; set; }
    DbSet<TodoEtapa> TodoEtapas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}