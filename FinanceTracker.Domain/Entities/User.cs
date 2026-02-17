namespace FinanceTracker.Domain.Entities;

public sealed class User
{
    private User() { }

    public User(string email, string passwordHash, DateTime createdUtc)
    {
        UserId = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        CreatedUtc = createdUtc;

        Profile = new Profile(UserId, "USD", email);
    }

    public Guid UserId { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public DateTime CreatedUtc { get; private set; }

    public Profile Profile { get; private set; } = null!;

    public ICollection<Budget> Budgets { get; private set; } = [];

    public ICollection<Category> Categories { get; private set; } = [];

    public ICollection<Transaction> Transactions { get; private set; } = [];

    public ICollection<Account> Accounts { get; private set; } = [];
}
