using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Features.Accounts;

public record CreateAccountDto(string Name, AccountType AccountType, string Currency, decimal Balance);
