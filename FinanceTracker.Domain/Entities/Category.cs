namespace FinanceTracker.Domain.Entities;

public sealed class Category
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid OwnerId { get; set; }

    public User Owner {  get; set; }

    public ICollection<Transaction> Transactions { get; set; }
}
