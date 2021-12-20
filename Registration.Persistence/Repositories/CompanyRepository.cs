using Registration.Domain.Entities.Companies;
using Registration.Persistence.Data;
using Registration.Persistence.Repositories.Common;

namespace Registration.Persistence.Repositories;

/// <summary>
/// Represents the <see cref="Company"/> repository (see <seealso cref="BaseRepository{Company}"/>) implementation of <see cref="ICompanyRepository"/>.
/// </summary>
public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CompanyRepository"/> class.
    /// </summary>
    /// <param name="context">The <see cref="RegistrationContext"/> database context.</param>
    public CompanyRepository(RegistrationContext context) : base(context)
    {
    }
}

