using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

public sealed class Transaction
{
    public Guid TransactionId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid? BudgetId { get; set; }

    public Guid OwnerId { get; set; }

    public Guid AccountId { get; set; }

    public decimal Amount { get; set; }

    public DateTime OccurredAtUtc { get; set; }

    public string Note { get; set; } = string.Empty;

    public string TransactionCurrency { get; set; } = string.Empty;

    public decimal ExchangeRate { get; set; }

    public CategoryType Type { get; set; }

    public User Owner { get; set; }

    public Budget? Budget { get; set; }

    public Account Account { get; set; }

    public Category Category { get; set; }
}
