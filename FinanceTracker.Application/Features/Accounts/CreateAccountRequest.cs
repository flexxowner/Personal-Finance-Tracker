using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Features.Accounts;

public record CreateAccountRequest(string Name, AccountType AccountType, string Currency, decimal Balance);
