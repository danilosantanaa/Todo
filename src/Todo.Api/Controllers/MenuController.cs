using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Todo.Application.Menus.Commands.Create;
using Todo.Application.Menus.Commands.Delete;
using Todo.Application.Menus.Commands.Update;
using Todo.Application.Menus.Queries.GetAll;
using Todo.Application.Menus.Queries.GetById;
using Todo.Contracts.Menu.Requests;
using Todo.Contracts.Menu.Response;

namespace Todo.Api.Controllers;

public class MenuController : ApiController
{
    public MenuController(
        ISender mediator,
        IMapper mapper) : base(mediator, mapper)
    { }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new MenuGetAllQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new MenuGetByIdQuery(id);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(MenuRequest request)
    {
        var command = _mapper.Map<MenuCreateCommand>(request);
        var result = await _mediator.Send(command);
        var response = _mapper.Map<MenuResponse>((result, command));
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, MenuRequest request)
    {
        var command = _mapper.Map<MenuUpdateCommand>((id, request));
        await _mediator.Send(command);
        return Ok(command);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new MenuDeleteCommand(id);
        await _mediator.Send(command);
        return Ok();
    }
}