using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Shared;
using FinanceTracker.Domain.Shared.Errors;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Accounts;

public class AccountService(IAppDbContext dbContext) : IAccountService
{
    public async Task<Result<Guid>> CreateAsync(CreateAccountDto request, Guid userId, CancellationToken cancellationToken)
    {
        var account = new Account(
            ownerId: userId,
            name: request.Name,
            currency: request.Currency,
            initialBalance: request.Balance,
            accountType: request.AccountType,
            createdUtc: DateTime.UtcNow);

        await dbContext.Accounts.AddAsync(account, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return account.AccountId;
    }

    public async Task<IReadOnlyCollection<AccountDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
        => await dbContext.Accounts
            .AsNoTracking()
            .Where(a => a.OwnerId == userId)
            .Where(a => a.IsActive)
            .Select(a => MapAccount(a))
            .ToListAsync(cancellationToken);

    public async Task<Result<AccountDto>> GetByIdAsync(Guid accountId, Guid userId, CancellationToken cancellationToken)
    {
        var account = await dbContext.Accounts
            .AsNoTracking()
            .Where(a => a.IsActive)
            .FirstOrDefaultAsync(a => a.OwnerId == userId && a.AccountId == accountId, cancellationToken);

        if (account is null)
        {
            return Result.Failure<AccountDto>(new EntityNotFound<Account>(accountId));
        }

        return MapAccount(account);
    }

    public async Task<Result> RemoveAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var accountToDelete = await dbContext.Accounts
            .FirstOrDefaultAsync(a => a.OwnerId == userId && a.AccountId == id, cancellationToken);

        if (accountToDelete is null)
        {
            return Result.Failure(new EntityNotFound<Account>(id));
        }

        accountToDelete.Archive();
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateAccountDto request, Guid userId, CancellationToken cancellationToken)
    {
        var accountToUpdate = await dbContext.Accounts
            .FirstOrDefaultAsync(a => a.OwnerId == userId && a.AccountId == id, cancellationToken);

        if (accountToUpdate is null)
        {
            return Result.Failure(new EntityNotFound<Account>(id));
        }

        accountToUpdate.Rename(request.Name);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private static AccountDto MapAccount(Account account) => new(account.AccountId)
    {
        Name = account.Name,
        AccountType = account.Type,
        Balance = account.Balance,
        Currency = account.Currency,
        CreatedUtc = account.CreatedUtc
    };
}
