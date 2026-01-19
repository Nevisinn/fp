namespace TagsCloud.Core.Models;

public class Result<TValue>
{
    private Result(TValue value)
    {
        Value = value;
    }

    private Result(string error)
    {
        Error = error;
        Value = default;
    }

    public string? Error { get; }
    public TValue? Value { get; }
    public bool IsSuccess => Error == null;

    public static Result<TValue> Ok(TValue value)
    {
        return new Result<TValue>(value);
    }

    public static Result<TValue> Fail(string e)
    {
        return new Result<TValue>(e);
    }
}

public class None
{
    private None()
    {
    }
}