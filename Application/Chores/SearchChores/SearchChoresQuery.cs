using Application.Abstractions.Messaging;
using Domain.Chores;

namespace Application.Chores.SearchChores;

public record SearchChoresQuery(
    string? Name,
    ChorePriority? Priority,
    ChoreFrequency? Frequency,
    ChoreCategory? Category,
    DayOfWeek? DayOfWeek) : IQuery;