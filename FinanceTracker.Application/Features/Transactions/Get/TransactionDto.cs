using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Features.Transactions.Get;

public record TransactionDto(Guid Id)
{
    public decimal Amount { get; init; }
    public DateTime OccurredAtUtc { get; init; }
    public string Note { get; init; } = string.Empty;
    public string TransactionCurrency { get; init; } = string.Empty;
    public decimal ExchangeRate { get; init; }
    public CategoryType Type { get; init; }
}
