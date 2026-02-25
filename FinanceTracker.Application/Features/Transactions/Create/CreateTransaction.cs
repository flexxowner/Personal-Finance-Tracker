using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Shared;
using FinanceTracker.Domain.Shared.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Transactions.Create;

public static class CreateTransaction
{
    public record Command : IRequest<Result<Guid>>
    {
        public decimal Amount { get; init; }
        public string? Note { get; init; }
        public required string TransactionCurrency { get; init; }
        public decimal ExchangeRate { get; init; }
        public CategoryType Type { get; init; }
        public DateTime OccurredAtUtc { get; init; }

        public Guid CategoryId { get; init; }
        public Guid? BudgetId { get; init; }
        public Guid OwnerId { get; init; }
        public Guid AccountId { get; init; }
    }

    public sealed class Handler(IAppDbContext context) : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var account = await context.Accounts
                .FirstOrDefaultAsync(a =>
                    a.AccountId == request.AccountId &&
                    a.OwnerId == request.OwnerId,
                    cancellationToken);

            if (account is null)
            {
                return Result.Failure<Guid>(new EntityNotFound<Account>(request.AccountId));
            }

            var categoryExists = await context.Categories
                .AsNoTracking()
                .AnyAsync(c => c.CategoryId == request.CategoryId, cancellationToken);

            if (!categoryExists)
            {
                return Result.Failure<Guid>(new EntityNotFound<Category>(request.CategoryId));
            }

            var transaction = new Transaction(
                ownerId: request.OwnerId,
                accountId: request.AccountId,
                categoryId: request.CategoryId,
                type: request.Type,
                amount: request.Amount,
                currency: request.TransactionCurrency,
                exchangeRate: request.ExchangeRate,
                occurredAt: request.OccurredAtUtc,
                note: request.Note
            );

            if (request.Type == CategoryType.Income)
            {
                account.Deposit(request.Amount);
            }
            else
            {
                try
                {
                    account.Withdraw(request.Amount);
                }
                catch (ArgumentException ex)
                {
                    return Result.Failure<Guid>(new ResultError(ErrorType.Validation, ex.Message));
                }
            }

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync(cancellationToken);

            return transaction.TransactionId;
        }
    }
}
