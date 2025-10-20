using Application.Abstractions.Messaging;
using Domain.Chores;
using Shared;

namespace Application.Chores.SearchChores;

public record SearchChoresQuery(
    string? Name,
    ChorePriority? Priority,
    ChoreFrequency? Frequency,
    ChoreCategory? Category,
    DayOfWeek? DayOfWeek,
    PaginationParams? PaginationParams) : IQuery;