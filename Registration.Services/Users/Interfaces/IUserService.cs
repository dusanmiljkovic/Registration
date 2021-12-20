using Registration.Domain.Entities.Users;
using Registration.Services.Users.Dto.Commands.DeleteUser;
using Registration.Services.Users.Dto.Commands.UpdateUser;
using Registration.Services.Users.Dto.Queries.GetUser;

namespace Registration.Services.Users.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Asynchronously gets the <see cref="User"/> record based on provided <paramref name="getUserCommand"/>.
    /// </summary>
    /// <param name="getUserCommand">The get <see cref="User"/> command.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="getUserCommand"/> is not set.</exception>
    Task<GetUserCommandResponse> GetUser(GetUserCommand getUserCommand);

    /// <summary>
    /// Asynchronously updates the <see cref="User"/> record based on provided <paramref name="updateUserCommand"/>.
    /// </summary>
    /// <param name="updateUserCommand">The update <see cref="User"/> command.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="updateUserCommand"/> is not set.</exception>
    Task<UpdateUserCommandResponse> UpdateUser(UpdateUserCommand updateUserCommand);

    /// <summary>
    /// Asynchronously deletes the <see cref="User"/> record based on provided <paramref name="deleteUserCommand"/>.
    /// </summary>
    /// <param name="deleteUserCommand">The delete <see cref="User"/> command.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="deleteUserCommand"/> is not set.</exception>
    Task DeleteUser(DeleteUserCommand deleteUserCommand);
}
