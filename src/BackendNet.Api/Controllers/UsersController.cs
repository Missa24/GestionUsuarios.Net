using BackendNet.Application.Users;
using BackendNet.Application.Users.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendNet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
    {
        var response = await _userService.GetAllAsync(page, pageSize);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        var response = await _userService.GetByIdAsync(id);
        return Ok(response); ;
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser(UserUpdateCommand command, Guid id)
    {
        var response = await _userService.UpdateUserAsync(command, id);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var response = await _userService.DeleteUserAsync(id);
        return Ok(response);
    }
}