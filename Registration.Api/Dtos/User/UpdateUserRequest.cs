using System.ComponentModel.DataAnnotations;

namespace Registration.Api.Dtos.User;
public class UpdateUserRequest
{
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
