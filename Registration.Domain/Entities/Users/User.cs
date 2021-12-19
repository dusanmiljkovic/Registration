using Registration.Domain.Base;
using Registration.Domain.Entities.Companies;

namespace Registration.Domain.Entities.Users;

/// <summary>
/// Represents the user domain model <seealso cref="BaseEntity"/>.
/// </summary>
public partial class User : BaseEntity
{
    /// <summary>
    /// Gets or sets username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets email.
    /// </summary>
    public string Email { get; set; } 

    /// <summary>
    /// Gets or sets company id.
    /// </summary>
    public long CompanyId { get; set; }

    /// <summary>
    /// Gets or sets company.
    /// </summary>
    public virtual Company Company { get; set; }
}
