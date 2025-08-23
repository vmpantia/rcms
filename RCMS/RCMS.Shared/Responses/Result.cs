using RCMS.Shared.Responses.Errors;

namespace RCMS.Shared.Responses;

public class Result<TData>
{
    public Result() { }
    
    private Result(TData data)
    {
        Data = data;
        Error = null;
        IsSuccess = true;
    }

    private Result(Error error)
    {
        Data = default;
        Error = error;
        IsSuccess = false;
    }

    public TData? Data { get; init; }
    public Error? Error { get; init; }
    public bool IsSuccess { get; init; }

    public static implicit operator Result<TData>(TData data) => new(data);
    public static implicit operator Result<TData>(Error error) => new(error);
}