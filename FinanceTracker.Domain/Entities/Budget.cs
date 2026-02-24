namespace FinanceTracker.Domain.Entities;

public sealed class Budget
{
    private Budget() { }

    public Budget(
        Guid ownerId, Guid categoryId,
        string currency, string name,
        DateTime periodStart, DateTime periodEnd,
        decimal limit)
    {
        if (limit <= 0) throw new ArgumentException("Limit must be positive");
        if (periodStart >= periodEnd) throw new ArgumentException("Start date must be before end date");

        BudgetId = Guid.NewGuid();
        OwnerId = ownerId;
        CategoryId = categoryId;
        Currency = currency;
        LimitAmount = limit;
        Name = name;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        UpdatedUtc = DateTime.UtcNow;
    }
    public Guid BudgetId { get; private set; }

    public Guid OwnerId { get; private set; }

    public Guid CategoryId { get; private set; }

    public DateTime CreatedUtc { get; private set; }

    public string Currency { get; private set; } = string.Empty;

    public decimal LimitAmount { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public bool IsDeleted { get; private set; }

    public DateTime PeriodStart { get; private set; }

    public DateTime PeriodEnd { get; private set; }

    public DateTime UpdatedUtc { get; private set; }

    public User Owner { get; private set; } = null!;
    public Category Category { get; private set; } = null!;

    public void UpdateLimit(decimal newLimit)
    {
        if (newLimit <= 0) throw new ArgumentException("Limit must be positive");

        LimitAmount = newLimit;
        UpdatedUtc = DateTime.UtcNow;
    }
    
    public void Delete()
    {
        if (IsDeleted) return;

        IsDeleted = true;
        UpdatedUtc = DateTime.UtcNow;
    }
    
    public void UpdateDetails(string name, decimal limit, DateTime start, DateTime end)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("Name cannot be empty");

        if (limit <= 0) 
            throw new ArgumentException("Limit must be positive");

        if (start >= end) 
            throw new ArgumentException("Start date must be before end date");
        
        Name = name;
        LimitAmount = limit;
        PeriodStart = start;
        PeriodEnd = end;
        UpdatedUtc = DateTime.UtcNow;
    }
}
