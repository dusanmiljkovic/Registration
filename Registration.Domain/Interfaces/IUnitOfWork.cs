using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;

namespace Registration.Domain.Interfaces;

/// <summary>
/// Represents <i>Unit of Work</i> pattern for working with repositories.
/// </summary>
/// <seealso cref="IDisposable"/>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the <see cref="IUserRepository"/> repository instance.
    /// </summary>
    IUserRepository UserRepository { get; }

    /// <summary>
    /// Gets the <see cref="ICompanyRepository"/> repository instance.
    /// </summary>
    ICompanyRepository CompanyRepository { get; }

    /// <summary>
    /// Saves the changes to database asynchronously with re-try strategy provided by <paramref name="options"/>.
    /// </summary>
    Task<int> SaveChangesAsync();
}
