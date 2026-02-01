using FinanceTracker.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected IActionResult HandleFailure(ResultError error)
    {
        return error.Type switch
        {
            ErrorType.None => throw new InvalidOperationException(),
            ErrorType.NullValue => BadRequest(new { error = error.Message }),
            ErrorType.Validation => BadRequest(new { error = error.Message }),
            ErrorType.NotFound => NotFound(new { error = error.Message }),
            ErrorType.Conflict => Conflict(new { error = error.Message }),
            _ => StatusCode(500, new { error = "An unexpected error occurred" }),
        };
    }
}
