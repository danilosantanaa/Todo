using MediatR;

using Todo.Application.Common.Interfaces.Persistence;
using Todo.Application.Menus.Common;

namespace Todo.Application.Menus.Queries.GetAll;

public sealed class MenuGetAllQueryHandler : IRequestHandler<MenuGetAllQuery, List<MenuResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public MenuGetAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<MenuResponse>> Handle(MenuGetAllQuery request, CancellationToken cancellationToken)
    {
        var menus = await _unitOfWork.MenuRepository.GetAllAsync(cancellationToken);

        return menus.Select(m =>
        {
            return new MenuResponse(m.Id.Value, m.Nome, m.IconUrl);
        })
        .ToList();
    }
}
