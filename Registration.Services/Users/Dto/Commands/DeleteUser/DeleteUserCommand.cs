using System.ComponentModel.DataAnnotations;

namespace Registration.Services.Users.Dto.Commands.DeleteUser;

/// <summary>
/// Defines layout of delete user command.
/// </summary>
public class DeleteUserCommand
{
    /// <summary>
    /// User id.
    /// </summary>
    [Required]
    public long UserId { get; set; }
}
