using FinanceTracker.Domain.Shared;

namespace FinanceTracker.Application.Features.Auth;

public interface IAuthService
{
    Task<Result<string>> RegisterAsync(AuthCredentialsRequest authCredentials, CancellationToken cancellationToken);
    Task<Result<string>> LoginAsync(AuthCredentialsRequest authCredentials, CancellationToken cancellationToken);
}
