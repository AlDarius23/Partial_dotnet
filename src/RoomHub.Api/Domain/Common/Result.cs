namespace RoomHub.Api.Domain.Common;

public sealed class Result<T>
{
    private readonly T? _value;

    private Result(T? value, Error? error, bool isSuccess)
    {
        _value = value;
        Error = error;
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; }

    public bool IsFailure
    {
        get
        {
            if (IsSuccess)
            {
                return false;
            }

            return true;
        }
    }

    public T Value
    {
        get
        {
            return _value!;
        }
    }

    public Error? Error { get; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, null, true);
    }

    public static Result<T> Failure(Error error)
    {
        return new Result<T>(default, error, false);
    }

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return Failure(error);
    }
}
