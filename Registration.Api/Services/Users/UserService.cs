using Registration.Api.DTOs.Users;
using Registration.Api.Exceptions;
using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;

namespace Registration.Api.Services.Users;
public class UserService : BaseService
{
    private readonly Serilog.ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(Serilog.ILogger logger, IUnitOfWork unitOfWork) 
        : base(logger, unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserResponse> GetUser(long userId)
    {
        User user = _unitOfWork.UserRepository.GetById(userId);
       
        if (user is null)
        {
            _logger.Error("User with ID \"{Id}\" was not found.", userId);
            throw new NotFoundException($"User with ID \"{userId}\" was not found.");
        }

        return new GetUserResponse {
            UserId = user.Id,
            Username = user.Username,
            CompanyId = user.CompanyId,
            Email = user.Email,
        };
    }

    public async Task UpdateUser(UpdateUserRequest updateUserRequest)
    {
        User user = _unitOfWork.UserRepository.GetById(updateUserRequest.UserId);

        if (user is null)
        {
            _logger.Error("User with ID \"{Id}\" was not found.", updateUserRequest.UserId);
            throw new NotFoundException($"User with ID \"{updateUserRequest.UserId}\" was not found.");
        }

        user.Update(updateUserRequest.Username, updateUserRequest.Email, updateUserRequest.Password);

        _unitOfWork.UserRepository.Update(user);
        await SaveChanges();
    }

    public async Task DeleteUser(long userId)
    {
        var user = _unitOfWork.UserRepository.GetById(userId);
        if (user is null)
        {
            throw new NotFoundException($"User with ID \"{userId}\" was not found.");
        }
        _unitOfWork.UserRepository.RemoveById(userId);

        var usersList = _unitOfWork.UserRepository.Find(u => u.CompanyId == user.CompanyId).ToList();
        if (usersList.Count == 1)
        {
            _unitOfWork.CompanyRepository.RemoveById(user.CompanyId);
        }
        await SaveChanges();
    }

    private async Task SaveChanges()
    {
        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new NotFoundException($"[{nameof(UserService)}] Error occured while saving to the database.");
        }
    }
}
