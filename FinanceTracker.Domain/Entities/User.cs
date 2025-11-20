namespace FinanceTracker.Domain.Entities;

public sealed class User
{
    public Guid UserId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedUtc { get; set; }

    public Profile Profile { get; set; } = null!;

    public ICollection<Budget> Budgets { get; set; }

    public ICollection<Category> Categories { get; set; }

    public ICollection<Transaction> Transactions { get; set; }

    public ICollection<Account> Accounts { get; set; }
}
