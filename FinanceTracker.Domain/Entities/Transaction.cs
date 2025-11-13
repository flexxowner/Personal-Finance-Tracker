namespace FinanceTracker.Domain.Entities;

public sealed class Transaction
{
    public Guid TransactionId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid BudgetId { get; set; }

    public Guid UserId { get; set; }

    public Guid AccountId { get; set; }

    public decimal Amount { get; set; }

    public string SourceCurrency { get; set; } = string.Empty;

    public DateTime OccurredAtUtc { get; set; }

    public string Note { get; set; } = string.Empty;

    public string TransactionCurrency { get; set; } = string.Empty;

    public decimal ExchangeRate { get; set; }
}
