namespace FinanceTracker.Application.Features.Budgets;

public record BudgetDto(Guid Id, Guid CategoryId, string Name, string Currency)
{
    public string? CategoryName {get; init;}
    public decimal LimitAmount { get; init; }
    public DateTime PeriodStart { get; init; }
    public DateTime PeriodEnd  { get; init; }
}