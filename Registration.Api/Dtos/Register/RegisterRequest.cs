using System.ComponentModel.DataAnnotations;

namespace Registration.Api.Dtos.Register;

/// <summary>
/// Register user request.
/// </summary>
public class RegisterRequest
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
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
    public string Email { get; set; }
}
