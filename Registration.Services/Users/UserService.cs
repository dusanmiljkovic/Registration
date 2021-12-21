﻿using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Services.Exceptions;
using Registration.Services.Users.Interfaces;
using Registration.Services.Users.Dto.Commands.DeleteUser;
using Registration.Services.Users.Dto.Commands.UpdateUser;
using Registration.Services.Users.Dto.Queries.GetUser;
using Registration.Shared.Extensions;
using Registration.Domain.Entities.Companies;

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
    public async Task<GetUserCommandResponse> GetUserAsync(GetUserCommand command)
    {
        Guard.ThrowIfNull(command, nameof(command));

        var user = _unitOfWork.UserRepository.Find(u => u.Id == command.UserId, u => u.Company).FirstOrDefault();
        if (user is null)
        {
            _logger.Error("User with ID \"{Id}\" was not found.", command.UserId);
            throw new NotFoundException($"User with ID \"{command.UserId}\" was not found.");
        }

        var response = new GetUserCommandResponse
        {
            UserId = user.Id,
            Username = user.Username,
            CompanyName = user.Company.Name,
            Email = user.Email,
        };
        return response;
    }

    /// <inheritdoc/>
    public async Task<UpdateUserCommandResponse> UpdateUserAsync(UpdateUserCommand command)
    {
        Guard.ThrowIfNull(command, nameof(command));

        var user = GetUserByUsernameOrEmailDifferentThanUserId(command.Username, command.Password, command.UserId);
        if (user is not null)
        {
            throw new UniqueException($"Email and password must be unique.");
        }

        user = _unitOfWork.UserRepository.GetById(command.UserId);
        if (user is null)
        {
            _logger.Error("User with ID \"{Id}\" was not found.", command.UserId);
            throw new NotFoundException($"User with ID \"{command.UserId}\" was not found.");
        }

        user.Update(command.Username, command.Password, command.Email);

        var company = GetCompanyByNameDifferentThanCompanyId(command.CompanyName, user.CompanyId);
        if (company is not null)
        {
            user.UpdateCompany(company.Id);
        }
        else
        {
            company = new Company(command.CompanyName);
            user.UpdateCompany(company);
            _logger.Information("Creating new company with name {Name}.", company.Name);
        }

        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
        _logger.Information("User with ID \"{Id}\" successfully updated.", user.Id);

        return new UpdateUserCommandResponse()
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            CompanyName = company.Name
        };
    }

    /// <inheritdoc/>
    public async Task DeleteUserAsync(DeleteUserCommand command)
    {
        Guard.ThrowIfNull(command, nameof(command));

        var user = _unitOfWork.UserRepository.GetById(command.UserId);
        if (user is null)
        {
            _logger.Error("User with ID \"{Id}\" was not found.", command.UserId);
            throw new NotFoundException($"User with ID \"{command.UserId}\" was not found.");
        }

        _unitOfWork.UserRepository.RemoveById(command.UserId);

        var usersList = _unitOfWork.UserRepository.Find(u => u.CompanyId == user.CompanyId).ToList();
        if (usersList.Count == 1)
        {
            _unitOfWork.CompanyRepository.RemoveById(user.CompanyId);
            _logger.Information("Company with ID \"{Id}\" successfully removed.", user.CompanyId);
        }
        _logger.Information("User with ID \"{Id}\" successfully removed.", user.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// Get user by esrname or email that is different than user id.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="email">Email.</param>
    /// <param name="userId">User id.</param>
    /// <returns>User instance.</returns>
    private User? GetUserByUsernameOrEmailDifferentThanUserId(string username, string email, long userId)
    {
        return _unitOfWork.UserRepository.Find(u => (u.Email.ToLower() == email.ToLower() || u.Username.ToLower() == username.ToLower()) && u.Id != userId).FirstOrDefault();
    }

    /// <summary>
    /// Get company by name different than company id.
    /// </summary>
    /// <param name="companyName"></param>
    /// <param name="companyId"></param>
    /// <returns></returns>
    private Company? GetCompanyByNameDifferentThanCompanyId(string companyName, long companyId)
    {
        return _unitOfWork.CompanyRepository.Find(c => c.Name == companyName && c.Id != companyId).FirstOrDefault();
    }
}
