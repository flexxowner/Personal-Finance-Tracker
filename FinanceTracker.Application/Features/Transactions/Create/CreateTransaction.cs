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

    public class Handler(IAppDbContext context) : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var accountExists = await context
                .Accounts
                .AsNoTracking()
                .AnyAsync(a => a.AccountId == request.AccountId, cancellationToken: cancellationToken);

            var categoryExists = await context
                .Categories
                .AsNoTracking()
                .AnyAsync(a => a.CategoryId == request.CategoryId, cancellationToken: cancellationToken);

            if (!accountExists)
            {
                return Result.Failure<Guid>(new EntityNotFound<Account>(request.AccountId));
            }

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
                note: request.Note);

           context.Transactions.Add(transaction);
           await context.SaveChangesAsync(cancellationToken);

           return transaction.TransactionId;
        }
    }
}
