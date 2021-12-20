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
    private readonly RegistrationService _service;
    private readonly ILogger<RegistrationController> _logger;

    public RegistrationController(ILogger<RegistrationController> logger,
        RegistrationService service)
    {
        _service = service.NotNull(nameof(service));
        _logger = logger.NotNull(nameof(logger));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
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
