using Application.Users;
using Domain.Chores;

namespace Application.Chores;

public static class ChoreMapping
{
    public static ChoreDto ToDto(this Chore chore)
    {
        return new ChoreDto
        {
            Id = chore.Id,
            Name = chore.Name,
            Priority = chore.Priority,
            Frequency = chore.Frequency,
            Category = chore.Category,
            DayOfWeek = chore.DayOfWeek,
            Assignees = chore.Assignees.Select(u => u.ToDto()).ToList()
        };
    }
}