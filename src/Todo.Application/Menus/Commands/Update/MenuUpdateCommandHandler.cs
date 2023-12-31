using System.Net;

using MediatR;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Menus.Errors;
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
        MenuId menuId = MenuId.Create(request.Id);

        var menu = await _unitOfWork.MenuRepository.GetByIdAsync(menuId, cancellationToken);
        if (menu is null)
            throw new MenuNotFoundException(HttpStatusCode.BadRequest);

        menu.Update(request.Nome, request.IconUrl);
        _unitOfWork.MenuRepository.Update(menu);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
