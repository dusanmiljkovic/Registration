using Registration.Domain.Entities.Users;
using Registration.Persistence.Data;
using Registration.Persistence.Repositories.Common;

namespace Registration.Persistence.Repositories;

/// <summary>
/// User repository class.
/// </summary>
public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(RegistrationContext context) : base(context)
    {
    }
}
