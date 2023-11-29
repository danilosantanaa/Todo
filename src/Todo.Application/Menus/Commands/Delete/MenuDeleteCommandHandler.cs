using MediatR;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Domain.Menus;
using Todo.Domain.Menus.Errors;
using Todo.Domain.Menus.ValueObjects;

namespace Todo.Application.Menus.Commands.Delete;

public sealed class MenuDeleteCommandHandler : IRequestHandler<MenuDeleteCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public MenuDeleteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(MenuDeleteCommand request, CancellationToken cancellationToken)
    {
        MenuId menuId = MenuId.Create(request.Id);
        Menu? menu = await _unitOfWork.MenuRepository.GetByIdAsync(menuId);

        if (menu is null)
        {
            throw new MenuNotFoundException();
        }

        _unitOfWork.MenuRepository.Delete(menu);
        await _unitOfWork.SaveChangesAsync();
    }
}
