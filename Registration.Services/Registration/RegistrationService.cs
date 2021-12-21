using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Services.Registration.Interfaces;
using Registration.Services.Registration.Dto.Commands.RegisterUser;
using Registration.Shared.Extensions;
using Registration.Services.Exceptions;

namespace Registration.Services.Registration;

/// <summary>
/// Service for management of user registration.
/// </summary>
/// <seealso cref="IRegistrationService"/>
public class RegistrationService : BaseService, IRegistrationService
{
    private readonly Serilog.ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationService"/> class.
    /// </summary> 
    /// <param name="logger">The logger instance.</param>
    /// <param name="unitOfWork">The Unit of Work instance.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> or <paramref name="unitOfWork"/> is not set.</exception>
    public RegistrationService(Serilog.ILogger logger, IUnitOfWork unitOfWork)
    : base(logger, unitOfWork)
    {
        _logger = logger.NotNull(nameof(logger));
        _unitOfWork = unitOfWork.NotNull(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task<RegisterUserCommandResponse> RegisterUserAsync(RegisterUserCommand command)
    {
        Guard.ThrowIfNull(command, nameof(command));
        var user = FindUserByEmailOrUsername(command.Email, command.Username);
        if (user is not null)
        {
            throw new UniqueException("Email and username must be unique.");
        }
        
        user = new User(command.Username, command.Password, command.Email);
        var company = FindCompanyByName(command.CompanyName);
        if (company is null)
        {
             company = new Company(command.CompanyName, user);
            _unitOfWork.CompanyRepository.Add(company);
            _logger.Information("New company with name {Id} saved successfully.", company.Name);
        }
        else
        {
            user.UpdateCompany(company.Id);
            _unitOfWork.UserRepository.Add(user);
        }

        await _unitOfWork.SaveChangesAsync();

        _logger.Information("New user with ID {Id} saved successfully.", user.Id);

        return new RegisterUserCommandResponse()
        {
            UserId = user.Id,
            CompanyName = company.Name,
            Username = user.Username,
            Email = user.Email
        };
    }

    /// <summary>
    /// Find company by name.
    /// </summary>
    /// <param name="companyName"></param>
    /// <returns>Company instance.</returns>
    private Company? FindCompanyByName(string companyName)
    {
        return _unitOfWork.CompanyRepository.Find(c => c.Name.ToLower() == companyName.ToLower()).FirstOrDefault();
    }

    /// <summary>
    /// Find user by email or username.
    /// </summary>
    /// <param name="email">Email.</param>
    /// <param name="username">Username.</param>
    /// <returns>User instance.</returns>
    private User? FindUserByEmailOrUsername(string email, string username)
    {
        return _unitOfWork.UserRepository.Find(u => u.Email.ToLower() == email.ToLower() || u.Username.ToLower() == username.ToLower()).FirstOrDefault();
    }
}
