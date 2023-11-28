using Microsoft.EntityFrameworkCore;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Menus;
using Todo.Domain.Menus.ValueObjects;
using Todo.Infrastructure.Persistence.Contexts;

namespace Todo.Infrastructure.Persistence.Repositories;

public class MenuRepository : Repository<Menu>, IMenuRepository
{
    public MenuRepository(TodoDatabaseContext context) : base(context) { }

    // TODO: Estudar a possibilidade de usar o do repositorio generico, invez do non-generico
    public async Task<Menu?> GetByIdAsync(MenuId id, CancellationToken cancellationToken = default)
    {
        return await _context.Menus.FirstOrDefaultAsync(m => m.Id == id);
    }
}