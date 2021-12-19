using Microsoft.AspNetCore.Mvc;
using Registration.Services.DTOs.Users;
using Registration.Services.Users;

namespace Registration.Api.Controllers;

/// <summary>
/// User controller.
/// </summary>
[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _service;
    private readonly ILogger<UserController> _logger;
    public UserController(ILogger<UserController> logger, 
        UserService service)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(long userId)
    {
        var user = await _service.GetUser(userId);
        return Ok(user);
    }

    [HttpPut("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(long userId, UpdateUserRequest updateUser)
    {
        if (userId != updateUser.UserId)
        {
            return BadRequest("Ids don't match.");
        }

        await _service.UpdateUser(updateUser);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(long userId)
    {
        await _service.DeleteUser(userId);
        return NoContent();
    }
}
