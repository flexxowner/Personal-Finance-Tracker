using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<Transaction> Transactions { get; }
    DbSet<Account> Accounts { get; }
    DbSet<Category> Categories { get; }
    DbSet<Budget> Budgets { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
