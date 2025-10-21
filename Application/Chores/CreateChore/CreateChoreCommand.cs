using Application.Abstractions.Messaging;
using Domain.Chores;

namespace Application.Chores.CreateChore;

public record CreateChoreCommand(
    Guid ChoreListId,
    string Name,
    string Description,
    DayOfWeek DayOfWeek,
    ChorePriority? Priority,
    ChoreFrequency? Frequency,
    ChoreCategory? Category) : ICommand;