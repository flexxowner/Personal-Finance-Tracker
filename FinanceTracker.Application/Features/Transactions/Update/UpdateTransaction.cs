using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Shared;
using FinanceTracker.Domain.Shared.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Transactions.Update;

public static class UpdateTransaction
{
    public record Command(Guid TransactionId) : IRequest<Result>
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

    public class Handler(IAppDbContext dbContext) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var transaction = await dbContext.Transactions
                .FirstOrDefaultAsync(t => t.TransactionId == request.TransactionId, cancellationToken);

            var accountExists = await dbContext.Accounts
                .AnyAsync(a => a.AccountId == request.AccountId, cancellationToken);

            var categoryExists = await dbContext.Categories
                .AnyAsync(a => a.CategoryId == request.CategoryId, cancellationToken);

            if (transaction is null)
            {
                return Result.Failure(new EntityNotFound<Transaction>(request.TransactionId));
            }

            if (transaction.OwnerId != request.OwnerId)
            {
                return Result.Failure(new EntityNotFound<Transaction>(request.TransactionId));
            }

            if (!accountExists)
            {
                return Result.Failure(new EntityNotFound<Account>(request.AccountId));
            }

            if (!categoryExists)
            {
                return Result.Failure(new EntityNotFound<Category>(request.CategoryId));
            }

            transaction.Update(
                amount: request.Amount,
                occurredAtUtc: request.OccurredAtUtc,
                transactionCurrency: request.TransactionCurrency,
                type: request.Type,
                exchangeRate: request.ExchangeRate,
                categoryId: request.CategoryId,
                accountId: request.AccountId,
                budgetId: request.BudgetId,
                note: request.Note);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
