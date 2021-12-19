using Registration.Services.Registration.Dto.Commands.RegisterUser;

namespace Registration.Services.Registration.Contracts
{
    public interface IRegistrationService
    {
        public Task<RegisterUserCommandResponse> RegisterUser(RegisterUserCommand registerUserCommand);
    }
}
