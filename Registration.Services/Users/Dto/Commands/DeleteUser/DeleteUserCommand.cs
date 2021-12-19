using System.ComponentModel.DataAnnotations;

namespace Registration.Services.Users.Dto.Commands.DeleteUser;
public class DeleteUserCommand
{
    [Required]
    public long UserId { get; set; }
}
