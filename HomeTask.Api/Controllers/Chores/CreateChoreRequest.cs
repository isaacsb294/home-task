using Domain.Chores;

namespace HomeTask.Api.Controllers.Chores;

public record CreateChoreRequest(
    Guid ChoreListId, 
    string Name, 
    string Description,
    DayOfWeek DayOfWeek,
    ChorePriority? Priority,
    ChoreFrequency? Frequency,
    ChoreCategory? Category);