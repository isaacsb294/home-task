using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Pagination;

public static class PaginationService
{
    public static async Task<PaginationResult<TDto>> GetResultAsync<T, TDto>( 
        IQueryable<T> query,
        PaginationParams paginationParams,
        Expression<Func<T, TDto>> mapperFunc,
        CancellationToken cancellationToken)
    {
        int totalCount = await query.CountAsync(cancellationToken);

        List<TDto> items = await query
            .Skip((paginationParams.Page - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .Select(mapperFunc)
            .ToListAsync(cancellationToken);

        return new PaginationResult<TDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = paginationParams.Page,
            PageSize = paginationParams.PageSize,
        };
    }
}