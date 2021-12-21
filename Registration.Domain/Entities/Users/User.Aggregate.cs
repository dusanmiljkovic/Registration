using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Registration.Domain.Base;
using Registration.Domain.Entities.Companies;
using System.Security.Cryptography;

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
        UpdatePassword(password);
        Email = email;
    }

    /// <summary>
    /// Update password.
    /// </summary>
    /// <param name="password">Password.</param>
    public void UpdatePassword(string password)
    {

        byte[] salt = new byte[128 / 8];
        using(var rng = new RNGCryptoServiceProvider())
        {
            rng.GetNonZeroBytes(salt);
        }

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        Password = hashed;
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
