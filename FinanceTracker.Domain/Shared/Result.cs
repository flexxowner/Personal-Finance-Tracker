namespace FinanceTracker.Domain.Shared;

public enum ErrorType
{
    None,
    NullValue,
    Validation,
    NotFound,
    Conflict
}

public record Result
{
    protected Result(bool isSuccess, ResultError error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public ResultError Error { get; }

    public static Result Success() => new(true, ResultError.None);

    public static Result Failure(ResultError error) => new(false, error);

    public static Result<T> Failure<T>(ResultError error) => Result<T>.Failure(error);
}

public record Result<T> : Result
{
    private readonly T? _value;

    private Result(T? value, bool isSuccess, ResultError error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result cannot be accessed.");


    public static Result<T> Success(T value) => new(value, true, ResultError.None);

    public static new Result<T> Failure(ResultError error) => new(default, false, error);

    public static implicit operator Result<T>(T? value) =>
        value is not null ? Success(value) : Failure(new ResultError(ErrorType.NullValue, "Value is null"));
}
