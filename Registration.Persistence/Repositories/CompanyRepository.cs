using Registration.Domain.Entities.Companies;
using Registration.Persistence.Data;
using Registration.Persistence.Repositories.Common;

namespace Registration.Persistence.Repositories;
public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    public CompanyRepository(RegistrationContext context) : base(context)
    {
    }
}

