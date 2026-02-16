namespace FinanceTracker.Application.Features.Auth;

public class AuthCredentialsDto
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
