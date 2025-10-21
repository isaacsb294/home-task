namespace Application.ChoreLists;

public sealed class ChoreListDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
};