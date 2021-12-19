namespace Registration.Services.Users.Dto.Queries.GetUser;
public class GetUserCommandResponse
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public long CompanyId { get; set; }
}
