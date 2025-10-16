using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Chores;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Chores.SearchChores;

public class SearchChoresQueryHandler(
    IApplicationDbContext dbContext,
    IUserContext  userContext) : IQueryHandler<SearchChoresQuery, List<Chore>>
{
    public async Task<Result<List<Chore>>> HandleAsync(SearchChoresQuery query, CancellationToken cancellationToken = default)
    {
        List<Chore> chores = await dbContext.Chores
            .AsNoTracking()
            .Where(c => c.UserId == userContext.UserId)
            .Where(c => query.Name == null ||
                        c.Name.Contains(query.Name, StringComparison.CurrentCultureIgnoreCase))
            .Where(c => query.Priority == null || c.Priority == query.Priority)
            .Where(c => query.Frequency == null || c.Frequency == query.Frequency)
            .Where(c => query.Category == null || c.Category == query.Category)
            .Where(c => query.DayOfWeek == null || c.DayOfWeek == query.DayOfWeek)
            .ToListAsync(cancellationToken);
        
        // TODO Paging

        return chores;
    }
}