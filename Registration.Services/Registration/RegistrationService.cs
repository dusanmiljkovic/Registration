using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Services.Registration.Interfaces;
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
        var company = new Company(registerUserCommand.CompanyName, user);

        _unitOfWork.CompanyRepository.Add(company);
        await _unitOfWork.SaveChangesAsync(); 

        return new RegisterUserCommandResponse()
        {
            CompanyName = company.Name,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        };
    }
}
