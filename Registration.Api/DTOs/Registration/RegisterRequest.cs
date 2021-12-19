using System.ComponentModel.DataAnnotations;

namespace Registration.Api.DTOs.Registration;
public class RegisterRequest
{
    [Required]
    [StringLength(50)]
    public string CompanyName { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }

    [Required]
    [StringLength(50)]
    public string Email { get; set; }
}
