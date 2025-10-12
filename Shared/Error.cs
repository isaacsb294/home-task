namespace Shared;

public record Error
{
    public string Code { get; init; }
    public string Message { get; init; }

    protected Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    
    public static Error NotFound(string message) =>
        new("Error.NotFound", message);
};