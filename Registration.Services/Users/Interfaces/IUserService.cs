using Registration.Domain.Entities.Users;
using Registration.Services.Users.Dto.Commands.DeleteUser;
using Registration.Services.Users.Dto.Commands.UpdateUser;
using Registration.Services.Users.Dto.Queries.GetUser;

namespace Registration.Services.Users.Interfaces;

/// <summary>
/// Provides management operations for the <see cref="UserService"/> domain model.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Asynchronously gets the <see cref="User"/> record based on provided <paramref name="command"/>.
    /// </summary>
    /// <param name="command">The get <see cref="User"/> command.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="command"/> is not set.</exception>
    Task<GetUserCommandResponse> GetUserAsync(GetUserCommand command);

    /// <summary>
    /// Asynchronously updates the <see cref="User"/> record based on provided <paramref name="command"/>.
    /// </summary>
    /// <param name="command">The update <see cref="User"/> command.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="command"/> is not set.</exception>
    Task<UpdateUserCommandResponse> UpdateUserAsync(UpdateUserCommand command);

    /// <summary>
    /// Asynchronously deletes the <see cref="User"/> record based on provided <paramref name="command"/>.
    /// </summary>
    /// <param name="command">The delete <see cref="User"/> command.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="command"/> is not set.</exception>
    Task DeleteUserAsync(DeleteUserCommand command);
}
