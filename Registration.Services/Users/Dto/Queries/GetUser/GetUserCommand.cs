using System.ComponentModel.DataAnnotations;

namespace Registration.Services.Users.Dto.Queries.GetUser;

/// <summary>
/// Defines layout of get user command.
/// </summary>
public class GetUserCommand
{
    /// <summary>
    /// User id.
    /// </summary>
    [Required]
    public long UserId { get; set; }
}
