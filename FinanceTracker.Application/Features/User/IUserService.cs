using FinanceTracker.Domain.Shared;

namespace FinanceTracker.Application.Features.User;

public interface IUserService
{
    Task<Result<UserDto>> GetUserById(Guid userId, CancellationToken cancellationToken);
}