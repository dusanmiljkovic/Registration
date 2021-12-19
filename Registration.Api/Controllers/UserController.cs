using Microsoft.AspNetCore.Mvc;
using Registration.Api.Dtos.User;
using Registration.Domain.Entities.Users;
using Registration.Services.Users;
using Registration.Services.Users.Dto.Commands.DeleteUser;
using Registration.Services.Users.Dto.Commands.UpdateUser;
using Registration.Services.Users.Dto.Queries.GetUser;

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
        GetUserCommand getUserCommand = new() { UserId = userId };
        GetUserCommandResponse user = await _service.GetUser(getUserCommand);
        return Ok(user);
    }

    [HttpPut("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(long userId, UpdateUserRequest updateUserRequest)
    {
        UpdateUserCommand updateUserCommand = new() { 
            UserId = userId, 
            Email = updateUserRequest.Email,
            Password = updateUserRequest.Password,
            Username = updateUserRequest.Username
        };
        var updatedUser = await _service.UpdateUser(updateUserCommand);
        return Ok(updatedUser);
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(long userId)
    {
        DeleteUserCommand command = new() { UserId = userId };
        await _service.DeleteUser(command);
        return NoContent();
    }
}
