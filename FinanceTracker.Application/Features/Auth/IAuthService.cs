using FinanceTracker.Domain.Shared;

namespace FinanceTracker.Application.Features.Auth;

public interface IAuthService
{
    Task<Result<string>> RegisterAsync(AuthCredentialsDto authCredentials, CancellationToken cancellationToken);
    Task<Result<string>> LoginAsync(AuthCredentialsDto authCredentials, CancellationToken cancellationToken);
}
