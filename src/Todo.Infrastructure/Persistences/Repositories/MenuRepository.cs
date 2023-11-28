using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Menus;
using Todo.Infrastructure.Persistence.Contexts;

namespace Todo.Infrastructure.Persistence.Repositories;

public class MenuRepository : Repository<Menu>, IMenuRepository
{
    public MenuRepository(TodoDatabaseContext context) : base(context) { }
}