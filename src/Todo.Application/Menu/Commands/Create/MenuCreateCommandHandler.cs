using MediatR;

using Todo.Application.Menu.Commands.Create;
using Todo.Application.Menu.Common;
using Todo.Domain.Menu.ValueObjects;

using MenuDomain = Todo.Domain.Menu.Menu;

namespace Todo.Application.Menu.Commands;

public sealed class MenuCreateCommandHandler : IRequestHandler<MenuCreateCommand, MenuResponse>
{
    public Task<MenuResponse> Handle(MenuCreateCommand request, CancellationToken cancellationToken)
    {
        var menuId = MenuId.Create();
        var menu = MenuDomain.Create(menuId, request.Nome, request.IconUrl);

        return Task.FromResult(new MenuResponse(menu.Id.Value, menu.Nome, menu.IconUrl));
    }
}