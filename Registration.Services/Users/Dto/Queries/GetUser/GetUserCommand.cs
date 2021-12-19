using System.ComponentModel.DataAnnotations;

namespace Registration.Services.Users.Dto.Queries.GetUser;
public class GetUserCommand
{
    [Required]
    public long UserId { get; set; }
}
