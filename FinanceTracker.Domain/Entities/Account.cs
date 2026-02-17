using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

public sealed class Account
{
    private Account() { }

    public Account(
        Guid ownerId, 
        string name,
        string currency,
        decimal initialBalance,
        AccountType accountType,
        DateTime createdUtc)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency required");

        AccountId = Guid.NewGuid();
        OwnerId = ownerId;
        Name = name;
        Currency = currency;
        Balance = initialBalance;
        Type = accountType;
        CreatedUtc = createdUtc;
        UpdatedUtc = createdUtc;
    }

    public Guid AccountId { get; private set; }

    public Guid OwnerId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public AccountType Type { get; set; }

    public decimal Balance { get; private set; }

    public string Currency { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    public DateTime CreatedUtc { get; private set; }

    public DateTime UpdatedUtc { get; private set; }

    public ICollection<Transaction> Transactions { get; private set; } = [];

    public User Owner { get; set; } = null!;

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount must be positive");
        Balance += amount;
        UpdatedUtc = DateTime.UtcNow;
    }

    public void Withdrow(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive");
        if (Balance - amount < 0) throw new ArgumentException("Balance amount must be positive");

        Balance -= amount;
        UpdatedUtc = DateTime.UtcNow;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Name required");

        Name = newName;
        UpdatedUtc = DateTime.UtcNow;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
