using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Features.Accounts;

public sealed record AccountDto(Guid Id)
{
    public required string Name { get; init; }
    public required string Currency { get; init; }
    public decimal Balance { get; init; }   
    public AccountType AccountType { get; init; }
    public DateTime CreatedUtc { get; init; }
    public int ActiveBudgetsCount { get; init; }
    public int TotalTransactionsCount { get; init; }

    public DateTime Created => CreatedUtc.ToLocalTime();
}