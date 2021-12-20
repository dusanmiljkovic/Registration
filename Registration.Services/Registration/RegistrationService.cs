using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Services.Registration.Interfaces;
using Registration.Services.Registration.Dto.Commands.RegisterUser;
using Registration.Shared.Extensions;

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
    public async Task<RegisterUserCommandResponse> RegisterUserAsync(RegisterUserCommand registerUserCommand)
    {
        Guard.ThrowIfNull(registerUserCommand, nameof(registerUserCommand));

        var user = new User(registerUserCommand.Username, registerUserCommand.Password, registerUserCommand.Email);
        var company = _unitOfWork.CompanyRepository.Find(c => c.Name.ToLower() == registerUserCommand.CompanyName.ToLower()).FirstOrDefault();
        if (company is null)
        {
             company = new Company(registerUserCommand.CompanyName, user);
            _unitOfWork.CompanyRepository.Add(company);
            _logger.Information("New company created.");
        }
        else
        {
            user.AddCompany(company.Id);
            _unitOfWork.UserRepository.Add(user);
        }

        await _unitOfWork.SaveChangesAsync();

        _logger.Information("New user with ID {Id} saved successfully.", user.Id);

        return new RegisterUserCommandResponse()
        {
            CompanyName = company.Name,
            Username = user.Username,
            Email = user.Email
        };
    }
}
