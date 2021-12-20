using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.Domain.Entities.Companies;

namespace Registration.Persistence.Configurations;

/// <summary>
/// Configures the <see cref="Company"/> entity.
/// </summary>
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    /// <summary>
    /// Configures the entity of type <see cref="Company"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable(nameof(Company));

        builder.HasKey(u => u.Id);

        builder
            .Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
