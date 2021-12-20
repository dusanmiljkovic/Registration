using Microsoft.EntityFrameworkCore;
using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Persistence.Data;
using Registration.Persistence.Repositories;

namespace Registration.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RegistrationContext _context;

        public UnitOfWork(RegistrationContext context)
        {
            _context = context;
            UserRepository = new UserRepository(context);
            CompanyRepository = new CompanyRepository(context);
        }


        #region IUnitOfWork implementation

        /// <inheritdoc/>
        public IUserRepository UserRepository { get; }

        /// <inheritdoc/>
        public ICompanyRepository CompanyRepository { get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion
    }
}
