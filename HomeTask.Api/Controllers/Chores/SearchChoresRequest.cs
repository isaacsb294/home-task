using Domain.Chores;
using Shared;

namespace HomeTask.Api.Controllers.Chores;

public record SearchChoresRequest(
    string? Search,
    ChorePriority? Priority,
    ChoreFrequency? Frequency,
    ChoreCategory? Category,
    DayOfWeek? DayOfWeek,
    PaginationParams? PaginationParams);