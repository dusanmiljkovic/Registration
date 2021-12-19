using Registration.Services.Users.Dto.Commands.DeleteUser;
using Registration.Services.Users.Dto.Commands.UpdateUser;
using Registration.Services.Users.Dto.Queries.GetUser;

namespace Registration.Services.Users.Contracts;

public interface IUserService
{
    public Task<GetUserCommandResponse> GetUser(GetUserCommand getUserCommand);
    public Task<UpdateUserCommandResponse> UpdateUser(UpdateUserCommand updateUserCommand);
    public Task DeleteUser(DeleteUserCommand deleteUserCommand);
}
