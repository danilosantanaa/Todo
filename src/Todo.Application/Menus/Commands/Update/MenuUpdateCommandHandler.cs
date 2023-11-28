using MediatR;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Application.Menus.Common;
using Todo.Domain.Menus.ValueObjects;

namespace Todo.Application.Menus.Commands.Update;

public sealed class MenuUpdateCommandHandler : IRequestHandler<MenuUpdateCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public MenuUpdateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(MenuUpdateCommand request, CancellationToken cancellationToken)
    {
        MenuId menuId = MenuId.Create(request.id);
        var menu = await _unitOfWork.MenuRepository.GetByIdAsync(menuId, cancellationToken);
        if (menu is null) throw new Exception("Menu não encontrado!");

        menu.Update(request.nome, request.IconUrl);
        _unitOfWork.MenuRepository.Update(menu);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
