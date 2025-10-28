using Domain.Chores;

namespace HomeTask.Api.Controllers.Chores;

public record EditChoreRequest(
    string? Name, 
    string? Description,
    ChorePriority? Priority,
    ChoreFrequency? Frequency,
    ChoreCategory? Category,
    DayOfWeek? DayOfWeek);