using Registration.Domain.Base;
using Registration.Domain.Entities.Companies;

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
        UpdateCompany(companyId);
    }

    /// <summary>
    /// Update user company.
    /// </summary>
    /// <param name="companyId">Company id.</param>
    public void UpdateCompany(long companyId)
    {
        CompanyId = companyId;
    }

    /// <summary>
    /// Update user company.
    /// </summary>
    /// <param name="company">Company.</param>
    public void UpdateCompany(Company company)
    {
        Company = company;
    }
}
