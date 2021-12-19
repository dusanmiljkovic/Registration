using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Services.Exceptions;
using Registration.Services.Registration.Contracts;
using Registration.Services.Registration.Dto.Commands.RegisterUser;

namespace Registration.Services.Registration;
public class RegistrationService : BaseService, IRegistrationService
{
    private readonly Serilog.ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrationService(Serilog.ILogger logger, IUnitOfWork unitOfWork)
    : base(logger, unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterUserCommandResponse> RegisterUser(RegisterUserCommand registerUserCommand)
    {
        var user = new User(registerUserCommand.Username, registerUserCommand.Password, registerUserCommand.Email);
        var company = new Company(registerUserCommand.CompanyName);
        var addedCompany = _unitOfWork.CompanyRepository.Add(company);
        await SaveChanges();

        user.AddCompany(addedCompany.Id);
        var addedUser = _unitOfWork.UserRepository.Add(user);

        await SaveChanges();

        return new RegisterUserCommandResponse()
        {
            CompanyName = company.Name,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        };
    }

    private async Task SaveChanges()
    {
        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new InvalidStateException($"[{nameof(RegistrationService)}] Error occured while saving to the database.");
        }
    }
}
