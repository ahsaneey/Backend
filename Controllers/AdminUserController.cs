using Calligraphy.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Api.Controllers;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
public class AdminUserController : ControllerBase
{
    private readonly IUserService _userService;

    public AdminUserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();

        var result = users.Select(user => new
        {
            user.Id,
            user.Name,
            user.Email,
            user.Role,
            user.IsBlocked
            
        });

        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
            return NotFound("User not found.");

        if (user.Role == "Admin")
            return NotFound("User not found.");

        return Ok(new
        {
            user.Id,
            user.Name,
            user.Email,
            user.Role
        });
    }
    [HttpPut("{id}/block")]
    public async Task<IActionResult> BlockUser(int id)
    {
        var blocked = await _userService.BlockAsync(id);

        if (!blocked)
            return NotFound("User not found.");

        return Ok(new
        {
            message = "User blocked successfully."
        });
    }
    [HttpPut("{id}/unblock")]
    public async Task<IActionResult> UnblockUser(int id)
    {
        var unblocked = await _userService.UnblockAsync(id);
        if (!unblocked)
            return NotFound("user not found");
        return Ok(new
        {
            message = "user unblocked succesfully."
        });
    }
}