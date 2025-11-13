namespace FinanceTracker.Domain.Entities;

public sealed class Budget
{
    public Guid BudgetId { get; set; }

    public Guid UserId { get; set; }

    public Guid CategoryId { get; set; }

    public DateTime CreatedUtc { get; set; }

    public string Currency { get; set; } = string.Empty;

    public decimal LimitAmount { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public DateTime PeriodStart { get; set; }

    public DateTime PeriodEnd { get; set; }

    public DateTime UpdatedUtc { get; set; }
}
