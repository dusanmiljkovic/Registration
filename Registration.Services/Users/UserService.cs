﻿using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Services.Exceptions;
using Registration.Services.Users.Interfaces;
using Registration.Services.Users.Dto.Commands.DeleteUser;
using Registration.Services.Users.Dto.Commands.UpdateUser;
using Registration.Services.Users.Dto.Queries.GetUser;
using Registration.Shared.Extensions;

namespace Registration.Services.Users;

/// <summary>
/// Service for management of <see cref="User"/> domain model.
/// </summary>
/// <seealso cref="IUserService"/>
public class UserService : BaseService, IUserService
{
    private readonly Serilog.ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="unitOfWork">The Unit of Work instance.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> or <paramref name="unitOfWork"/> is not set.</exception>
    public UserService(Serilog.ILogger logger, IUnitOfWork unitOfWork)
        : base(logger, unitOfWork)
    {
        _logger = logger.NotNull(nameof(logger));
        _unitOfWork = unitOfWork.NotNull(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task<GetUserCommandResponse> GetUser(GetUserCommand getUserCommand)
    {
        Guard.ThrowIfNull(getUserCommand, nameof(getUserCommand));

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

    /// <inheritdoc/>
    public async Task<UpdateUserCommandResponse> UpdateUser(UpdateUserCommand updateUserCommand)
    {
        Guard.ThrowIfNull(updateUserCommand, nameof(updateUserCommand));

        var user = _unitOfWork.UserRepository.GetById(updateUserCommand.UserId);

        if (user is null)
        {
            _logger.Error("User with ID \"{Id}\" was not found.", updateUserCommand.UserId);
            throw new NotFoundException($"User with ID \"{updateUserCommand.UserId}\" was not found.");
        }

        user.Update(updateUserCommand.Username, updateUserCommand.Email, updateUserCommand.Password);

        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();

        _logger.Information("User with ID \"{Id}\" successfully updated.", user.Id);

        return new UpdateUserCommandResponse()
        {
            Username = user.Username,
            Email = user.Email
        };
    }

    /// <inheritdoc/>
    public async Task DeleteUser(DeleteUserCommand deleteUserCommand)
    {
        Guard.ThrowIfNull(deleteUserCommand, nameof(deleteUserCommand));

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
            _logger.Information("Company with ID \"{Id}\" successfully removed.", user.CompanyId);
        }
        _logger.Information("User with ID \"{Id}\" successfully removed.", user.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
