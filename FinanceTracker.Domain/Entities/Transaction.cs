using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

public sealed class Transaction
{
    public Guid TransactionId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid? BudgetId { get; private set; }
    public Guid OwnerId { get; private set; }
    public Guid AccountId { get; private set; }

    public decimal Amount { get; private set; }
    public DateTime OccurredAtUtc { get; private set; }
    public string Note { get; private set; } = string.Empty;
    public string TransactionCurrency { get; private set; } = string.Empty;
    public decimal ExchangeRate { get; private set; }
    public CategoryType Type { get; private set; }

    public User Owner { get; private set; }
    public Budget? Budget { get; private set; }
    public Account Account { get; private set; }
    public Category Category { get; private set; }

    private Transaction() { }

    public Transaction(
        Guid ownerId,
        Guid accountId,
        Guid categoryId,
        CategoryType type,
        decimal amount,
        string currency,
        decimal exchangeRate,
        DateTime occurredAt,
        string? note,
        Guid? budgetId = null)
    {
        if (amount <= 0)
            throw new ArgumentException("The transaction amount must be greater than zero.", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is mandatory", nameof(currency));

        if (exchangeRate <= 0)
            throw new ArgumentException("The exchange rate must be positive", nameof(exchangeRate));

        if (accountId == Guid.Empty) throw new ArgumentException("Account not specified", nameof(accountId));
        if (categoryId == Guid.Empty) throw new ArgumentException("Category not specified", nameof(categoryId));
        if (ownerId == Guid.Empty) throw new ArgumentException("Owner not specified", nameof(ownerId));

        TransactionId = Guid.NewGuid();
        OwnerId = ownerId;
        AccountId = accountId;
        CategoryId = categoryId;
        Type = type;
        Amount = amount;
        TransactionCurrency = currency.ToUpper();
        ExchangeRate = exchangeRate;
        OccurredAtUtc = occurredAt;
        Note = note ?? string.Empty;
        BudgetId = budgetId;
    }

    public void UpdateNote(string? note) => Note = note ?? string.Empty;
}
