using FinanceTracker.Domain.Shared;

namespace FinanceTracker.Application.Features.Accounts;

public interface IAccountService
{
    Task<Result<AccountDto>> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<AccountDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<Result> RemoveAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(Guid id, UpdateAccountDto request, Guid userId, CancellationToken cancellationToken);
    Task<Result<Guid>> CreateAsync(CreateAccountDto request, Guid userId, CancellationToken cancellationToken);
}
