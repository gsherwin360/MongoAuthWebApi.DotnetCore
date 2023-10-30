using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoAuthWebApi.Commands;
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
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var loginCommand = new LoginCommand(loginModel.Email, loginModel.Password);
        var result = await Mediator.Send(loginCommand);

        if (result.IsSuccess) return Ok(result.Value);
        return BadRequest(result.Error);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        var registerCommand = new RegisterCommand(registerModel.Email, registerModel.Password);

        var result = await Mediator.Send(registerCommand);

        if (result.IsSuccess) return Ok(result.Value);
        return BadRequest(result.Error);
    }
}