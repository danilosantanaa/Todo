using MediatR;

using Todo.Application.Menus.Common;

namespace Todo.Application.Menus.Queries.GetAll;

public record MenuGetAllQuery() : IRequest<List<MenuResponse>>;