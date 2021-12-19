using Microsoft.AspNetCore.Mvc;
using Registration.Api.Services.Users;

namespace Registration.Api.Controllers;

/// <summary>
/// Registration controller.
/// </summary>
[Route("api/register")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<RegistrationController> _logger;
    public RegistrationController(ILogger<RegistrationController> logger,
        UserService service)
    {
        _userService = service;
        _logger = logger;
    }
}
