using Registration.Services.Registration.Dto.Commands.RegisterUser;

namespace Registration.Services.Registration.Interfaces;
public interface IRegistrationService
{
    public Task<RegisterUserCommandResponse> RegisterUser(RegisterUserCommand registerUserCommand);
}
