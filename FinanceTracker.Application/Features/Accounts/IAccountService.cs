using FinanceTracker.Domain.Shared;

namespace FinanceTracker.Application.Features.Accounts;

public interface IAccountService
{
    Task<Result<AccountDto>> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<AccountDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<Result> RemoveAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(Guid id, UpdateAccountRequest request, Guid userId, CancellationToken cancellationToken);
    Task<Result<Guid>> CreateAsync(CreateAccountRequest request, Guid userId, CancellationToken cancellationToken);
}
