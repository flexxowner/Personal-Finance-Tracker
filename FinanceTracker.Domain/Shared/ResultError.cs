namespace FinanceTracker.Domain.Shared;

public record ResultError(ErrorType Type, string Details)
{
    public string? Title { get; init; }

    public IReadOnlyCollection<string> ErrorMessages { get; init; } = [];

    public static readonly ResultError None = new(ErrorType.None, string.Empty);
}
