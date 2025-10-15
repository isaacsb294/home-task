namespace Shared;

public record Error
{
    public string Code { get; init; }
    public string Message { get; init; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Null value provided");
    public static Error NotFound(string message) =>
        new("Error.NotFound", message);
};