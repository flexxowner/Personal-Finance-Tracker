using FinanceTracker.Application.Features.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ApiControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] AuthCredentialsDto request, CancellationToken cancellationToken)
    {
        var registrationResult = await authService.RegisterAsync(request, cancellationToken);
        if (!registrationResult.IsSuccess)
        {
            return HandleFailure(registrationResult);
        }

        return Ok(new { token = registrationResult.Value });
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] AuthCredentialsDto request, CancellationToken cancellationToken)
    {
        var loginResult = await authService.LoginAsync(request, cancellationToken);
        if (!loginResult.IsSuccess)
        {
            return HandleFailure(loginResult);
        }

        return Ok(new { token = loginResult.Value });
    }
}
