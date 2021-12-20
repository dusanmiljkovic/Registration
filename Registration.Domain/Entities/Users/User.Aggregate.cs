using Registration.Domain.Base;

namespace Registration.Domain.Entities.Users;

/// <summary>
/// User aggregate class.
/// </summary>
public partial class User : IAggregateRoot
{
    /// <summary>
    /// User constructor.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    /// <param name="email">Email.</param>
    public User(string username,
        string password,
        string email)
    {
        Update(
            username,
            password,
            email);
    }

    /// <summary>
    /// Update user.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    /// <param name="email">Email.</param>
    public void Update(string username,
        string password,
        string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }

    /// <summary>
    /// Add company to user.
    /// </summary>
    /// <param name="companyId">Company id.</param>
    public void AddCompany(long companyId)
    {
        CompanyId = companyId;
    }
}
