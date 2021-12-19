using System.ComponentModel.DataAnnotations;

namespace Registration.Services.Users.Dto.Commands.UpdateUser;
public class UpdateUserCommand
{
    [Required]
    public long UserId { get; set; }

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
