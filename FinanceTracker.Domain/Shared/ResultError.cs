namespace FinanceTracker.Domain.Shared;

public record ResultError(ErrorType Type, string Message)
{
    public static readonly ResultError None = new(ErrorType.None, string.Empty);
}
