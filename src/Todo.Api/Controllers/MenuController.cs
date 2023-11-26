using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Todo.Application.Menus.Commands.Create;
using Todo.Contracts.Menu.Requests;

namespace Todo.Api.Controllers;

public class MenuController : ApiController
{
    public MenuController(
        ISender mediator,
        IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(MenuRequest request)
    {
        throw new Exception("Error");

        var command = _mapper.Map<MenuCreateCommand>(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}