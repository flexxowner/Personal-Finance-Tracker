using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Infrastructure.Data;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions.Create;

public class CreateTransaction
{
    public record Command : IRequest<Guid>
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

    public class Handler(AppDbContext context) : IRequestHandler<Command, Guid>
    {
        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            var transaction = new Transaction(
                ownerId: request.OwnerId,
                accountId: request.AccountId,
                categoryId: request.CategoryId,
                type: request.Type,
                amount: request.Amount,
                currency: request.TransactionCurrency,
                exchangeRate: request.ExchangeRate,
                occurredAt: request.OccurredAtUtc,
                note: request.Note,
                budgetId: request.BudgetId);

           context.Transactions.Add(transaction);
           await context.SaveChangesAsync(cancellationToken);

           return transaction.TransactionId;
        }
    }
}
