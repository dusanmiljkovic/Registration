using Microsoft.AspNetCore.Mvc;
using Registration.Api.Dtos.Register;
using Registration.Services.Registration;
using Registration.Services.Registration.Dto.Commands.RegisterUser;

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
        _service = service;
        _logger = logger;
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
        var registeredUser = await _service.RegisterUser(registerUserCommand);
        return Ok(registeredUser);
    }
}
