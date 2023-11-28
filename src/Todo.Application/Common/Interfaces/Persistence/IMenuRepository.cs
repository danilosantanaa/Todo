using Todo.Domain.Menus;
using Todo.Domain.Menus.ValueObjects;

namespace Todo.Application.Common.Interfaces.Persistence;

public interface IMenuRepository : IRepository<Menu>
{
    Task<Menu?> GetByIdAsync(MenuId menuId, CancellationToken cancellationToken = default);
}