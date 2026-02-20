namespace FinanceTracker.Application.Features.User;

public record UserDto(Guid Id, string Email, string UserName, string Currency)
{
    public decimal Balance { get; init; }
}