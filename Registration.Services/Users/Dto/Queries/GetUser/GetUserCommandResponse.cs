namespace Registration.Services.Users.Dto.Queries.GetUser;

/// <summary>
/// Defines layout of get user response.
/// </summary>
public class GetUserCommandResponse
{
    /// <summary>
    /// User id.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Company id.
    /// </summary>
    public long CompanyId { get; set; }
}
