using MediatR;

using Todo.Application.Menus.Common;

namespace Todo.Application.Menus.Queries.GetById;

public record MenuGetByIdQuery(Guid id) : IRequest<MenuResponse?>;