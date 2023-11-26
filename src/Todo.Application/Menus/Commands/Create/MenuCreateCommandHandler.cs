using MediatR;

using Todo.Application.Menus.Commands.Create;
using Todo.Application.Menus.Common;
using Todo.Domain.Menus.ValueObjects;

using Todo.Domain.Menus;

namespace Todo.Application.Menus.Commands;

public sealed class MenuCreateCommandHandler : IRequestHandler<MenuCreateCommand, MenuResponse>
{
    public Task<MenuResponse> Handle(MenuCreateCommand request, CancellationToken cancellationToken)
    {
        var menuId = MenuId.Create();
        var menu = Menu.Create(menuId, request.Nome, request.IconUrl);

        return Task.FromResult(new MenuResponse(menu.Id.Value, menu.Nome, menu.IconUrl));
    }
}