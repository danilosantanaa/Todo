using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender _mediator;
    protected readonly IMapper _mapper;

    protected ApiController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

}