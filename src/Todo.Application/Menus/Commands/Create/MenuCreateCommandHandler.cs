using MediatR;

using Todo.Application.Menus.Commands.Create;
using Todo.Application.Menus.Common;
using Todo.Domain.Menus.ValueObjects;

using Todo.Domain.Menus;
using Todo.Application.Common.Interfaces.Persistence;

namespace Todo.Application.Menus.Commands.Create;

public sealed class MenuCreateCommandHandler : IRequestHandler<MenuCreateCommand, MenuResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public MenuCreateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<MenuResponse> Handle(MenuCreateCommand request, CancellationToken cancellationToken)
    {
        var menuId = MenuId.Create();
        var menu = Menu.Create(menuId, request.Nome, request.IconUrl);

        _unitOfWork.MenuRepository.Add(menu);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MenuResponse(menu.Id.Value, menu.Nome, menu.IconUrl);
    }
}