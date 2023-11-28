using MediatR;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Application.Menus.Common;
using Todo.Domain.Menus.ValueObjects;

namespace Todo.Application.Menus.Queries.GetById;

public sealed class MenuGetByIdQueryHandler : IRequestHandler<MenuGetByIdQuery, MenuResponse?>
{
    private readonly IUnitOfWork _unitOfWork;

    public MenuGetByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    async Task<MenuResponse?> IRequestHandler<MenuGetByIdQuery, MenuResponse?>.Handle(MenuGetByIdQuery request, CancellationToken cancellationToken)
    {
        MenuId menuId = MenuId.Create(request.id);
        var menu = await _unitOfWork.MenuRepository.GetByIdAsync(menuId);

        return new MenuResponse(menu!.Id.Value, menu.Nome, menu.IconUrl);
    }
}
