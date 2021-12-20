﻿using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Services.Exceptions;
using Registration.Services.Users.Interfaces;
using Registration.Services.Users.Dto.Commands.DeleteUser;
using Registration.Services.Users.Dto.Commands.UpdateUser;
using Registration.Services.Users.Dto.Queries.GetUser;

namespace Registration.Services.Users;

public class UserService : BaseService, IUserService
{
    private readonly Serilog.ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(Serilog.ILogger logger, IUnitOfWork unitOfWork)
        : base(logger, unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserCommandResponse> GetUser(GetUserCommand getUserCommand)
    {
        User? user = _unitOfWork.UserRepository.GetById(getUserCommand.UserId);

        if (user is null)
        {
            _logger.Error("User with ID \"{Id}\" was not found.", getUserCommand.UserId);
            throw new NotFoundException($"User with ID \"{getUserCommand.UserId}\" was not found.");
        }

        var response = new GetUserCommandResponse
        {
            UserId = user.Id,
            Username = user.Username,
            CompanyId = user.CompanyId,
            Email = user.Email,
        };
        return response;
    }

    public async Task<UpdateUserCommandResponse> UpdateUser(UpdateUserCommand updateUserCommand)
    {
        var user = _unitOfWork.UserRepository.GetById(updateUserCommand.UserId);

        if (user is null)
        {
            _logger.Error("User with ID \"{Id}\" was not found.", updateUserCommand.UserId);
            throw new NotFoundException($"User with ID \"{updateUserCommand.UserId}\" was not found.");
        }

        user.Update(updateUserCommand.Username, updateUserCommand.Email, updateUserCommand.Password);

        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return new UpdateUserCommandResponse()
        {
            Username = user.Username,
            Email = user.Email
        };
    }

    public async Task DeleteUser(DeleteUserCommand deleteUserCommand)
    {
        var user = _unitOfWork.UserRepository.GetById(deleteUserCommand.UserId);
        if (user is null)
        {
            _logger.Error("User with ID \"{Id}\" was not found.", deleteUserCommand.UserId);
            throw new NotFoundException($"User with ID \"{deleteUserCommand.UserId}\" was not found.");
        }

        _unitOfWork.UserRepository.RemoveById(deleteUserCommand.UserId);

        var usersList = _unitOfWork.UserRepository.Find(u => u.CompanyId == user.CompanyId).ToList();
        if (usersList.Count == 1)
        {
            _unitOfWork.CompanyRepository.RemoveById(user.CompanyId);
        }
        await _unitOfWork.SaveChangesAsync();
    }
}
