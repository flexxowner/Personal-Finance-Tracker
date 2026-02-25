using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Shared;
using FinanceTracker.Domain.Shared.Errors;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Budgets;

public class BudgetService(IAppDbContext dbContext) : IBudgetService
{
    public async Task<IReadOnlyCollection<BudgetDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        var budgets = await dbContext.Budgets
            .AsNoTracking()
            .Where(b => b.OwnerId == userId)
            .Select(b => new BudgetDto(b.BudgetId, b.CategoryId, b.Name, b.Owner.Profile.DefaultCurrency)
            {
                CategoryName = b.Category.Name,
                PeriodEnd = b.PeriodEnd,
                PeriodStart = b.PeriodStart,
                LimitAmount = b.LimitAmount,
                SpentAmount = dbContext.Transactions
                    .Where(t => t.OwnerId == userId)
                    .Where(t => t.CategoryId == b.CategoryId)
                    .Where(t => t.OccurredAtUtc >= b.PeriodStart && t.OccurredAtUtc <= b.PeriodEnd)
                    .Where(t => t.Type == CategoryType.Expense)
                    .Sum(t => t.Amount)
            })
            .ToListAsync(cancellationToken);

        return budgets;
    }

    public async Task<Result<BudgetDto>> GetByIdAsync(Guid budgetId, Guid userId, CancellationToken cancellationToken)
    {
        var budgetDto = await dbContext.Budgets
            .AsNoTracking()
            .Where(b => b.BudgetId == budgetId && b.OwnerId == userId)
            .Select(b => new BudgetDto(b.BudgetId, b.CategoryId, b.Name, b.Owner.Profile.DefaultCurrency)
            {
                CategoryName = b.Category.Name,
                PeriodEnd = b.PeriodEnd,
                PeriodStart = b.PeriodStart,
                LimitAmount = b.LimitAmount,
                SpentAmount = dbContext.Transactions
                    .Where(t => t.OwnerId == userId)
                    .Where(t => t.CategoryId == b.CategoryId)
                    .Where(t => t.OccurredAtUtc >= b.PeriodStart && t.OccurredAtUtc <= b.PeriodEnd)
                    .Where(t => t.Type == CategoryType.Expense)
                    .Sum(t => t.Amount)
            })
            .FirstOrDefaultAsync(cancellationToken);

        return budgetDto ?? Result.Failure<BudgetDto>(new EntityNotFound<Budget>(budgetId));
    }

    public async Task<Result<Guid>> CreateAsync(CreateBudgetRequest request, Guid userId, CancellationToken cancellationToken)
    {
        var currency = await dbContext.Users
            .AsNoTracking()
            .Where(u => u.UserId == userId)
            .Select(u => u.Profile.DefaultCurrency)
            .FirstOrDefaultAsync(cancellationToken);

        if (currency is null)
        {
            return Result.Failure<Guid>(new EntityNotFound<User>(userId));
        }

        var newBudget = new Budget(
            ownerId: userId,
            name: request.Name,
            periodStart: request.PeriodStart,
            periodEnd: request.PeriodEnd,
            categoryId: request.CategoryId,
            currency: currency,
            limit: request.Limit);

        await dbContext.Budgets.AddAsync(newBudget, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return newBudget.BudgetId;
    }

    public async Task<Result> UpdateAsync(Guid budgetId, UpdateBudgetRequest request, Guid userId,
        CancellationToken cancellationToken)
    {
        var budget = await dbContext.Budgets
            .FirstOrDefaultAsync(b => b.BudgetId == budgetId && b.OwnerId == userId, cancellationToken);

        if (budget is null)
        {
            return Result.Failure(new EntityNotFound<Budget>(budgetId));
        }

        budget.UpdateDetails(
            name: request.Name,
            start: request.PeriodStart,
            end: request.PeriodEnd,
            limit: request.Limit);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid budgetId, Guid userId, CancellationToken cancellationToken)
    {
        var budgetToDelete = await dbContext.Budgets
            .FirstOrDefaultAsync(b => b.BudgetId == budgetId && b.OwnerId == userId, cancellationToken);

        if (budgetToDelete is null)
        {
            return Result.Failure(new EntityNotFound<Budget>(budgetId));
        }

        budgetToDelete.Delete();
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
