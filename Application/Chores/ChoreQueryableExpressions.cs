using System.Linq.Expressions;
using Application.Users;
using Domain.Chores;

namespace Application.Chores;

public static class ChoreQueryableExpressions
{
    public static Expression<Func<Chore, ChoreDto>> ProjectToDto()
    {
        return chore => new ChoreDto
        {
            Id = chore.Id,
            Name = chore.Name,
            Priority = chore.Priority,
            Frequency = chore.Frequency,
            Category = chore.Category,
            DayOfWeek = chore.DayOfWeek,
            Assignees = chore.Assignees.Select(a => new UserDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                EmailAddress = string.Empty
            }).ToList()
        };
    }
}