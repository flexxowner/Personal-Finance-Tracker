namespace FinanceTracker.Domain.Entities;

public sealed class Category
{
    private Category() { }

    public Category(string name, Guid ownerId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");

        CategoryId = Guid.NewGuid();
        Name = name;
        OwnerId = ownerId;
    }

    public Guid CategoryId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public Guid OwnerId { get; private set; }

    public User Owner { get; private set; } = null!;

    public ICollection<Transaction> Transactions { get; set; } = [];
}
