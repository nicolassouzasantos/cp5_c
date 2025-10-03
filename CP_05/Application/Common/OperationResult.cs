namespace CP_05.Application.Common;

public enum OperationErrorType
{
    None = 0,
    NotFound,
    Validation,
    Conflict,
    Unknown
}

public class OperationResult
{
    protected OperationResult(bool isSuccess, OperationErrorType errorType, IEnumerable<string> errors)
    {
        IsSuccess = isSuccess;
        ErrorType = errorType;
        Errors = errors.ToArray();
    }

    public bool IsSuccess { get; }

    public OperationErrorType ErrorType { get; }

    public IReadOnlyCollection<string> Errors { get; }

    public static OperationResult Success() => new(true, OperationErrorType.None, Array.Empty<string>());

    public static OperationResult Failure(OperationErrorType errorType, params string[] errors)
        => new(false, errorType, errors);
}

public class OperationResult<T> : OperationResult
{
    private OperationResult(bool isSuccess, OperationErrorType errorType, T? data, IEnumerable<string> errors)
        : base(isSuccess, errorType, errors)
    {
        Data = data;
    }

    public T? Data { get; }

    public static OperationResult<T> Success(T data)
        => new(true, OperationErrorType.None, data, Array.Empty<string>());

    public static OperationResult<T> Failure(OperationErrorType errorType, params string[] errors)
        => new(false, errorType, default, errors);
}
