using MediatR;

namespace Todo.Application.Menus.Commands.Delete;

public record MenuDeleteCommand(Guid Id) : IRequest;