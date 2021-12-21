using Microsoft.EntityFrameworkCore;
using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Persistence.Data;
using Registration.Persistence.Repositories;
using Registration.Shared.Extensions;

namespace Registration.Persistence
{
    /// <summary>
    /// UnitOfWork class.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RegistrationContext _context;

        /// <summary>
        /// Unit of work contstructor.
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(RegistrationContext context)
        {
            _context = context.NotNull(nameof(context));
            UserRepository = new UserRepository(context);
            CompanyRepository = new CompanyRepository(context);
        }


        #region IUnitOfWork implementation

        /// <inheritdoc/>
        public IUserRepository UserRepository { get; }

        /// <inheritdoc/>
        public ICompanyRepository CompanyRepository { get; }

        /// <inheritdoc/>
        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Save changes async.
        /// </summary>
        /// <returns></returns>
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
