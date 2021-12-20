namespace Registration.Services.Registration.Dto.Commands.RegisterUser;

/// <summary>
/// Defines the layout of register user response.
/// </summary>
public class RegisterUserCommandResponse
{
    /// <summary>
    /// User id.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Company name.
    /// </summary>
    public string CompanyName { get; set; }

    /// <summary>
    /// Username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; set; }
}

