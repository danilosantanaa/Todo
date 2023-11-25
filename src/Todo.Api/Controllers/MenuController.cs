using System.ComponentModel.Design;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Todo.Application.Menu.Commands.Create;
using Todo.Contracts.Menu.Requests;

namespace Todo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public MenuController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(MenuRequest request)
    {
        var command = _mapper.Map<MenuCreateCommand>(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}