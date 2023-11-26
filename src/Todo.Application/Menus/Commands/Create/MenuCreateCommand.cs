using MediatR;

using Todo.Application.Menus.Common;

namespace Todo.Application.Menus.Commands.Create;

public record MenuCreateCommand(string Nome, string IconUrl) : IRequest<MenuResponse>;