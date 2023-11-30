using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Todo.Application.Todos.Commands.Create;
using Todo.Contracts.Todo.Request;

namespace Todo.Api.Controllers;

public sealed class TodoController : ApiController
{
    public TodoController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(TodoRequest request)
    {
        var command = _mapper.Map<TodoCreateCommand>(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}