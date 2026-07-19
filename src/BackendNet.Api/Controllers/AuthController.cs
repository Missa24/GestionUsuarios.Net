using System.IdentityModel.Tokens.Jwt;
using BackendNet.Application.Auth.Register;
using BackendNet.Application.Auth.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BackendNet.Application.Users;
using System.Security.Claims;
namespace BackendNet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserHandler _registerUserHandler;
    private readonly LoginUserHandler _loginUserHandler;
    private readonly UserService _userService;

    public AuthController(RegisterUserHandler registerUserHandler, LoginUserHandler loginUserHandler, UserService userService)
    {
        _registerUserHandler = registerUserHandler;
        _loginUserHandler = loginUserHandler;
        _userService = userService;

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var response = await _registerUserHandler.Handle(command);

        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var response = await _loginUserHandler.Handle(command);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            return Unauthorized();
        }

        var id = Guid.Parse(userId);

        var response = await _userService.GetByIdAsync(id);

        return Ok(response);
    }
}