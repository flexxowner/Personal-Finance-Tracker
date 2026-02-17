using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Features.Transactions.Create;

public sealed record CreateTransactionDto(
    Guid AccountId,
    Guid CategoryId,
    decimal Amount,
    string Currency,
    DateTime OccurredAtUtc,
    CategoryType Type,
    string? Note)
{
    public Guid? BudgetId { get; init; }

    public decimal ExchangeRate { get; init; } = 1.0m;
}
