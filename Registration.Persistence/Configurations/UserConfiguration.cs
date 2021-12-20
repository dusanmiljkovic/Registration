using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.Domain.Entities.Users;

namespace Registration.Persistence.Configurations;

/// <summary>
/// Configures the <see cref="User"/> entity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the entity of type <see cref="User"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasKey(u => u.Id);

        builder
            .Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasOne(e => e.Company)
            .WithMany(s => s.Users)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();

    }
}
