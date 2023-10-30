using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoAuthWebApi.Commands;
using MongoAuthWebApi.DTOs;
using MongoAuthWebApi.Models;

namespace MongoAuthWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var loginCommand = new LoginCommand(loginModel.Email, loginModel.Password);
        var result = await Mediator.Send(loginCommand);

        if (result.IsSuccess) return Ok(result.Value);
        return BadRequest(result.Error);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        var registerCommand = new RegisterCommand(registerModel.Email, registerModel.Password);

        var result = await Mediator.Send(registerCommand);

        if (result.IsSuccess) return CreatedAtAction(nameof(this.Register), result.Value);
        return BadRequest(result.Error);
    }
}