﻿namespace Registration.Services.Users.Dto.Commands.UpdateUser;

/// <summary>
/// Defines layout of update user response.
/// </summary>
public class UpdateUserCommandResponse
{
    /// <summary>
    /// User id.
    /// </summary>
    public long UserId { get; set; }
    /// <summary>
    /// Username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Company name.
    /// </summary>
    public string CompanyName { get; set; }
}
