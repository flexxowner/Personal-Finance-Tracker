namespace FinanceTracker.Domain.Shared.Errors;

public record TransactionNotFound(Guid Id)
{
    public string Message => $"Transaction with ID {Id} was not found.";

    public static implicit operator ResultError(TransactionNotFound error)
    {
        return new ResultError(ErrorType.NotFound, error.Message);
    }
}
