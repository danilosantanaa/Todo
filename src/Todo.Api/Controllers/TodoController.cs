using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Todo.Application.Todos.Commands.Create;
using Todo.Application.Todos.Commands.Update;
using Todo.Application.Todos.Queries.GetAll;
using Todo.Application.Todos.Queries.GetById;
using Todo.Contracts.Todo.Request;
using Todo.Contracts.Todo.Response;

namespace Todo.Api.Controllers;

public sealed class TodoController : ApiController
{
    public TodoController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    { }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new TodoGetAllQuery();
        var result = await _mediator.Send(query);
        var todoResponse = _mapper.Map<List<TodoResponse>>(result);

        return Ok(todoResponse);
    }

    [HttpGet("{todoId:guid}")]
    public async Task<IActionResult> GetById(Guid todoId)
    {
        var query = new TodoGetByIdQuery(todoId);
        var result = await _mediator.Send(query);
        var todoResponse = _mapper.Map<TodoResponse>(result);

        return Ok(todoResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TodoCreateRequest request)
    {
        var command = _mapper.Map<TodoCreateCommand>(request);
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPut("{todoId:guid}")]
    public async Task<IActionResult> Update(Guid todoId, TodoUpdateRequest request)
    {
        var command = _mapper.Map<TodoUpdateCommand>((todoId, request));
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}