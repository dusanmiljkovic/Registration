using System.ComponentModel.DataAnnotations;

namespace Registration.Services.Registration.Dto.Commands.RegisterUser;
public class RegisterUserCommand
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
