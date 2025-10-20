using Application.Users;
using Domain.Chores;

namespace Application.Chores;

public sealed class ChoreDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public ChorePriority Priority { get; init; }
    public ChoreFrequency Frequency { get; init; }
    public ChoreCategory Category { get; init; }
    public DayOfWeek DayOfWeek { get; init; }
    public List<UserDto> Assignees { get; init; } = [];
};