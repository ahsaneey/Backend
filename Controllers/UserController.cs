using System.Security.Claims;
<<<<<<< HEAD
using Calligraphy.Application.Interfaces.Services;
=======
>>>>>>> 99b30f21cfc0eb191daf1a64225c8f3cd3d30208
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
<<<<<<< HEAD
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();

        var user = await _userService.GetByIdAsync(userId);

        if (user == null)
            return NotFound();

        return Ok(user);
=======
    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Ok(new
        {
            UserId = userId
        });
>>>>>>> 99b30f21cfc0eb191daf1a64225c8f3cd3d30208
    }
}