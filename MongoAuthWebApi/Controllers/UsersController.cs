using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoAuthWebApi.DTOs;
using MongoAuthWebApi.Queries;

namespace MongoAuthWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await Mediator.Send(query);

        return Ok(result);
    }
}
