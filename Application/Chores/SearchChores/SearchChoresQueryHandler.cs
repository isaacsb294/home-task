using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Application.Pagination;
using Domain.Chores;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Chores.SearchChores;

public class SearchChoresQueryHandler(
    IApplicationDbContext dbContext) : IQueryHandler<SearchChoresQuery, PaginationResult<ChoreDto>>
{
    public async Task<Result<PaginationResult<ChoreDto>>> HandleAsync(
        SearchChoresQuery query,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Chore> choresQueryable = dbContext.Chores
            .AsNoTracking()
            .Include(c => c.Assignees)
            .AsNoTracking()
            .Include(c => c.Products)
            .AsNoTracking()
            .Where(c => query.Name == null ||
                        c.Name.Contains(query.Name, StringComparison.CurrentCultureIgnoreCase))
            .Where(c => query.Priority == null || c.Priority == query.Priority)
            .Where(c => query.Frequency == null || c.Frequency == query.Frequency)
            .Where(c => query.Category == null || c.Category == query.Category)
            .Where(c => query.DayOfWeek == null || c.DayOfWeek == query.DayOfWeek);

        PaginationResult<ChoreDto> paginationResult = await PaginationService.GetResultAsync(
            choresQueryable,
            query.PaginationParams ?? PaginationParams.Default ,
            ChoreQueryableExpressions.ProjectToDto(),
            cancellationToken);

        return paginationResult;
    }
}