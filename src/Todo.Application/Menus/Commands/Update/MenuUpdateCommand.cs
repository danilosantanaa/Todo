using MediatR;

using Todo.Application.Menus.Common;

namespace Todo.Application.Menus.Commands.Update;

public record MenuUpdateCommand(Guid id, string nome, string IconUrl) : IRequest;