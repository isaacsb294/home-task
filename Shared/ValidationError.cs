namespace Shared;

public sealed record ValidationError(Error[] Errors) :
    Error("Validation.General",
    "One or more validation errors occurred")
{
    public static ValidationError FromResults(IEnumerable<Result> results) =>
        new(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());
}
