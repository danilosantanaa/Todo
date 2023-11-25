using MediatR;

using Todo.Application.Menu.Common;

namespace Todo.Application.Menu.Commands.Create;

public record MenuCreateCommand(string Nome, string IconUrl) : IRequest<MenuResponse>;