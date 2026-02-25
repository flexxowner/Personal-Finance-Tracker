using FinanceTracker.Application.Features.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ApiControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] AuthCredentialsRequest request, CancellationToken cancellationToken)
    {
        var registrationResult = await authService.RegisterAsync(request, cancellationToken);
        if (!registrationResult.IsSuccess)
        {
            return HandleFailure(registrationResult);
        }

        return registrationResult.IsSuccess ? Ok(new { token = registrationResult.Value }) : HandleFailure(registrationResult);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] AuthCredentialsRequest request, CancellationToken cancellationToken)
    {
        var loginResult = await authService.LoginAsync(request, cancellationToken);

        return loginResult.IsSuccess ? Ok(new { token = loginResult.Value }) : HandleFailure(loginResult);
    }
}
