using Registration.Domain.Entities.Users;
using Registration.Persistence.Data;
using Registration.Persistence.Repositories.Common;

namespace Registration.Persistence.Repositories;

/// <summary>
/// Represents the <see cref="User"/> repository (see <seealso cref="BaseRepository{User}"/>) implementation of <see cref="IUserRepository"/>.
/// </summary>
public class UserRepository : BaseRepository<User>, IUserRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="context">The <see cref="RegistrationContext"/> database context.</param>
    public UserRepository(RegistrationContext context) 
        : base(context)
    {
    }
}
