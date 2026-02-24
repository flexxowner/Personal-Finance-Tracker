using FinanceTracker.Domain.Shared;

namespace FinanceTracker.Application.Features.Budgets;

public interface IBudgetService
{
    public Task<IReadOnlyCollection<BudgetDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    public Task<Result<BudgetDto>> GetByIdAsync(Guid budgetId, Guid userId, CancellationToken cancellationToken);
    public Task<Result<Guid>> CreateAsync(CreateBudgetRequest request, Guid userId, CancellationToken cancellationToken);
    public Task<Result> UpdateAsync(Guid budgetId, UpdateBudgetRequest request, Guid userId, CancellationToken cancellationToken);
    public Task<Result> DeleteAsync(Guid budgetId, Guid userId, CancellationToken cancellationToken);
}