namespace FinanceTracker.Application.Features.Budgets;

public record CreateBudgetRequest(Guid AccountId, Guid CategoryId, string Name, DateTime PeriodStart, DateTime PeriodEnd, decimal Limit);