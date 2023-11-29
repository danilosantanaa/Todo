using MediatR;

namespace Todo.Application.Menus.Commands.Update;

public record MenuUpdateCommand(Guid Id, string Nome, string IconUrl) : IRequest;