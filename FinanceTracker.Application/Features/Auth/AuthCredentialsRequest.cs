namespace FinanceTracker.Application.Features.Auth;

public class AuthCredentialsRequest
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
