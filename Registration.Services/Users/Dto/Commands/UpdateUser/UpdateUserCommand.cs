using System.ComponentModel.DataAnnotations;

namespace Registration.Services.Users.Dto.Commands.UpdateUser;

/// <summary>
/// Defines layout of update user command.
/// </summary>
public class UpdateUserCommand
{
    /// <summary>
    /// User id.
    /// </summary>
    [Required]
    public long UserId { get; set; }

    /// <summary>
    /// Username.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Password { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Email { get; set; }

    /// <summary>
    /// Company name.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string? CompanyName { get; set; }
}
