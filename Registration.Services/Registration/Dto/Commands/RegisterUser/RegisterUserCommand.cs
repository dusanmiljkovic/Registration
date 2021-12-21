using System.ComponentModel.DataAnnotations;

namespace Registration.Services.Registration.Dto.Commands.RegisterUser;

/// <summary>
/// Defines layout of register user command.
/// </summary>
public class RegisterUserCommand
{
    /// <summary>
    /// Company name.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string CompanyName { get; set; }

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
}
