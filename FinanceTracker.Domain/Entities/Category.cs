using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

public sealed class Category
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public CategoryType Type { get; set; }

    public Guid OwnerId { get; set; }
}
