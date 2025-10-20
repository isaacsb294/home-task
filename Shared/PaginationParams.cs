namespace Shared;

public record PaginationParams(
    int Page,
    int PageSize)
{
    public static PaginationParams Default => new(1, 100);
};