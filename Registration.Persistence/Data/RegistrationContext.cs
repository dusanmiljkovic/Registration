using Microsoft.EntityFrameworkCore;
using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;
using System.Reflection;

namespace Registration.Persistence.Data;

/// <summary>
/// RegistrationContext class.
/// </summary>
public class RegistrationContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by a DbContext.</param>
    public RegistrationContext(DbContextOptions<RegistrationContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// DB set of company entities.
    /// </summary>
    public DbSet<Company> Companies { get; set; } = null!;

    /// <summary>
    /// DB set of user entities.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Customizes the ASP.NET Identity model and overrides the defaults.
    /// </summary>
    /// <param name="builder">Model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}
