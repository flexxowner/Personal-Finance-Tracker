using System.Security.Claims;
using FinanceTracker.Application.Features.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class AccountController(IUserService userService) : ApiControllerBase
{
    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult> GetMe(CancellationToken cancellationToken)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString) ||  !Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Invalid token");
        }
        
        var result = await userService.GetUserById(userId, cancellationToken);
        if (!result.IsSuccess)
        {
            return HandleFailure(result);
        }
        
        return Ok(result);
    }
}