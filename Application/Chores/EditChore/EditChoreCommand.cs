using Application.Abstractions.Messaging;
using Domain.Chores;

namespace Application.Chores.EditChore;

public record EditChoreCommand(
    Guid ChoreId, 
    string? Name, 
    string? Description,
    DayOfWeek? DayOfWeek,
    ChorePriority? Priority,
    ChoreFrequency? Frequency,
    ChoreCategory? Category) : ICommand;