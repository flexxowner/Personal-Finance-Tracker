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
            ErrorType.NullValue => BadRequest(CreateDetailedResult(error, StatusCodes.Status400BadRequest)),
            ErrorType.Validation => BadRequest(CreateDetailedResult(error, StatusCodes.Status400BadRequest)),
            ErrorType.NotFound => NotFound(CreateDetailedResult(error, StatusCodes.Status404NotFound)),
            ErrorType.Conflict => Conflict(CreateDetailedResult(error, StatusCodes.Status409Conflict)),
            _ => StatusCode(StatusCodes.Status500InternalServerError, CreateDetailedResult(error, StatusCodes.Status500InternalServerError)),
        };
    }

    private static ProblemDetails CreateDetailedResult(ResultError error, int status)
    {
        return new ProblemDetails
        {
            Title = error.Title,
            Detail = error.Details,
            Status = status,
            Extensions = { { "errors:", error.ErrorMessages } }
        };
    }
}
