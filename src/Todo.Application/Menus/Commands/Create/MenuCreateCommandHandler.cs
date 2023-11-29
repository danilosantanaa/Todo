using MediatR;
using Todo.Application.Menus.Common;
using Todo.Domain.Menus.ValueObjects;

using Todo.Domain.Menus;
using Todo.Application.Common.Interfaces.Persistence;

namespace Todo.Application.Menus.Commands.Create;

public sealed class MenuCreateCommandHandler : IRequestHandler<MenuCreateCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public MenuCreateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(MenuCreateCommand request, CancellationToken cancellationToken)
    {
        var menuId = MenuId.Create();
        var menu = Menu.Create(menuId, request.Nome, request.IconUrl);

        _unitOfWork.MenuRepository.Add(menu);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return menu.Id.Value;
    }
}