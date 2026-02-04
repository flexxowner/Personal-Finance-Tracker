namespace FinanceTracker.Domain.Shared.Errors;

public record EntityNotFound<TEntity>(Guid Id)
{
    public static implicit operator ResultError(EntityNotFound<TEntity> error)
    {
        var entityName = typeof(TEntity).Name;
        var message = $"{entityName} with ID {error.Id} was not found.";

        return new ResultError(ErrorType.NotFound, message);
    }
}
