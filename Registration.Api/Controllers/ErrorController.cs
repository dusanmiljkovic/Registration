using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Registration.Services.Exceptions;
using Registration.Shared.Extensions;

namespace Registration.Api.Controllers;

/// <summary>
/// ErrorController class.
/// </summary>
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    /// <summary>
    /// Provides logger instance. 
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorController"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger.NotNull(nameof(logger));
    }

    /// <summary>
    /// Exception handler.
    /// </summary>
    /// <returns>Status code with exception information.</returns>
    [Route("/error")]
    public IActionResult Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;
        var message = exception?.Message;

        if (message != null)
        {
            _logger.LogError(message);
            switch (exception)
            {
                case UniqueException:
                    return StatusCode(400, message);
                case NotFoundException:
                    return StatusCode(404, message);
                default:
                    return StatusCode(500, message);
            }
        }

        return StatusCode(500, $"[{nameof(ErrorController)}] Unexpected server error occured. Log message is not found.");
    }
}
