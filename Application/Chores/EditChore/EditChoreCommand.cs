using Application.Abstractions.Messaging;
using Domain.Chores;

namespace Application.Chores.EditChore;

public record EditChoreCommand(
    Guid ChoreId, 
    string? Name, 
    string? Description,
    ChorePriority? Priority,
    ChoreFrequency? Frequency,
    ChoreCategory? Category,
    DayOfWeek? DayOfWeek) : ICommand;