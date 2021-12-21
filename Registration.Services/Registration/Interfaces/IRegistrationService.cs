using Registration.Services.Registration.Dto.Commands.RegisterUser;

namespace Registration.Services.Registration.Interfaces;

/// <summary>
/// Provides management operations for the <see cref="RegistrationService"/> domain model.
/// </summary>
public interface IRegistrationService
{
    /// <summary>
    /// Asynchronously registers new user record based on <paramref name="command"/>.
    /// Returns instance of saved entyty with a task that represents an asynchronous operation.
    /// </summary>
    /// <param name="command">Contains information for registering new user.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="command"/> is not set.</exception>
    Task<RegisterUserCommandResponse> RegisterUserAsync(RegisterUserCommand command);
}
