namespace ECommerce.Common.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new InvalidOperationException("Success result cannot have an error");

        if (!isSuccess && error == Error.None)
            throw new InvalidOperationException("Failure result must have an error");

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(isSuccess: true, error: Error.None);
    public static Result Failure(Error error) => new(isSuccess: false, error: error);
    public static Result<T> Success<T>(T value) => new(value, isSuccess: true, error: Error.None);
    public static Result<T> Failure<T>(Error error) => new(default, isSuccess: false, error: error);
}

public class Result<T> : Result
{
    public T? Value { get; }

    protected internal Result(T? value, bool isSuccess, Error error)
        : base(isSuccess, error) => Value = value;
}
