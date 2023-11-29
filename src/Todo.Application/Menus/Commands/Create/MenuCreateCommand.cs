using MediatR;

namespace Todo.Application.Menus.Commands.Create;

public record MenuCreateCommand(string Nome, string IconUrl) : IRequest<Guid>;