using Application.Abstractions.Messaging;
using Domain.Chores;

namespace Application.Chores.CreateChore;

public record CreateChoreCommand(
    Guid UserId,
    Guid ChoreListId,
    string Name,
    string Description,
    ChorePriority? Priority,
    ChoreFrequency? Frequency,
    ChoreCategory? Category,
    DayOfWeek? DayOfWeek) : ICommand;