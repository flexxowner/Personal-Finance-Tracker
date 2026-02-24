namespace FinanceTracker.Application.Features.Budgets;

public record UpdateBudgetRequest(
    string Name,
    decimal Limit,
    DateTime PeriodStart,
    DateTime PeriodEnd
);