using Microsoft.AspNetCore.Mvc;
using Registration.Api.Dtos.User;
using Registration.Services.Users;
using Registration.Services.Users.Dto.Commands.DeleteUser;
using Registration.Services.Users.Dto.Commands.UpdateUser;
using Registration.Services.Users.Dto.Queries.GetUser;
using Registration.Shared.Extensions;

namespace Registration.Api.Controllers;

/// <summary>
/// User controller.
/// </summary>
[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    /// <summary>
    /// User service instance.
    /// </summary>
    private readonly UserService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="service">Provides user service instance.</param>
    public UserController(UserService service)
    {
        _service = service.NotNull(nameof(service));
    }

    /// <summary>
    /// Get user.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <returns>Get user response.</returns>
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(long userId)
    {
        GetUserCommand getUserCommand = new() { UserId = userId };
        GetUserCommandResponse user = await _service.GetUser(getUserCommand);
        return Ok(user);
    }

    /// <summary>
    /// Update user.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="updateUserRequest">Update user request.</param>
    /// <returns>Updated user.</returns>
    [HttpPut("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(long userId, UpdateUserRequest updateUserRequest)
    {
        UpdateUserCommand updateUserCommand = new() { 
            UserId = userId, 
            Email = updateUserRequest.Email,
            Password = updateUserRequest.Password,
            Username = updateUserRequest.Username
        };
        UpdateUserCommandResponse updatedUser = await _service.UpdateUser(updateUserCommand);
        return Ok(updatedUser);
    }

    /// <summary>
    /// Delete user.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <returns>Information if vehicle is deleted.</returns>
    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(long userId)
    {
        DeleteUserCommand command = new() { UserId = userId };
        await _service.DeleteUser(command);
        return NoContent();
    }
}
