using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Todo.Application.Menus.Commands.Create;
using Todo.Application.Menus.Commands.Update;
using Todo.Application.Menus.Queries.GetAll;
using Todo.Application.Menus.Queries.GetById;
using Todo.Contracts.Menu.Requests;

namespace Todo.Api.Controllers;

public class MenuController : ApiController
{
    public MenuController(
        ISender mediator,
        IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new MenuGetAllQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var command = new MenuGetByIdQuery(id);
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(MenuRequest request)
    {
        var command = _mapper.Map<MenuCreateCommand>(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, MenuRequest request)
    {
        var command = _mapper.Map<MenuUpdateCommand>((id, request));
        await _mediator.Send(command);

        return Ok(command);
    }
}