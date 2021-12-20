using Microsoft.AspNetCore.Mvc;
using Registration.Api.Dtos.Register;
using Registration.Services.Registration;
using Registration.Services.Registration.Dto.Commands.RegisterUser;
using Registration.Shared.Extensions;

namespace Registration.Api.Controllers;

/// <summary>
/// Registration controller.
/// </summary>
[Route("api/register")]
[ApiController]
public class RegistrationController : ControllerBase
{
    /// <summary>
    /// Registration service instance.
    /// </summary>
    private readonly RegistrationService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationService"/> class.
    /// </summary>
    /// <param name="service">Provides race service instance.</param>
    public RegistrationController(RegistrationService service)
    {
        _service = service.NotNull(nameof(service));
    }

    /// <summary>
    /// Register new user.
    /// </summary>
    /// <param name="registerRequest">Register new user request.</param>
    /// <returns>Register user information.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        RegisterUserCommand registerUserCommand = new() { 
            CompanyName = registerRequest.CompanyName,
            Username = registerRequest.Username,
            Password = registerRequest.Password,
            Email = registerRequest.Email
        };
        RegisterUserCommandResponse registeredUser = await _service.RegisterUser(registerUserCommand);
        return Created("", registeredUser);
    }
}
