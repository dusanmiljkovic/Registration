using Registration.Domain.Base;
using Registration.Domain.Entities.Users;

namespace Registration.Domain.Entities.Companies;

/// <summary>
/// Company aggreagate class.
/// </summary>
public partial class Company : IAggregateRoot
{
    /// <summary>
    /// Company constructor.
    /// </summary>
    /// <param name="name">Company name.</param>
    /// <param name="user">User.</param>
    public Company(string name)
    {
        Update(name);
    }

    /// <summary>
    /// Company constructor.
    /// </summary>
    /// <param name="name">Company name.</param>
    /// <param name="user">User.</param>
    public Company(string name, User user)
    {
        Update(name);
        AddUser(user);
    }

    /// <summary>
    /// Update company.
    /// </summary>
    /// <param name="name">Company name.</param>
    public void Update(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Add user to company.
    /// </summary>
    /// <param name="user">User to be added.</param>
    public void AddUser(User user)
    {
        Users.Add(user);
    }
}
