using Registration.Domain.Base;
using Registration.Domain.Entities.Users;

namespace Registration.Domain.Entities.Companies;

/// <summary>
/// Represents the company domain model <seealso cref="BaseEntity"/>.
/// </summary>
public partial class Company : BaseEntity
{
    /// <summary>
    /// Gets or sets name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets users.
    /// </summary>
    public virtual ICollection<User> Users{ get; set; } = new HashSet<User>();
}
