using MediatR;

namespace Todo.Application.Todos.Commands.Delete;

public record TodoDeleteCommand(Guid todoId) : IRequest;