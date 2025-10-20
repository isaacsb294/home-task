using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.ChoreLists;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.ChoreLists.GetChoreList;

public class GetChoreListQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetChoreListQuery, ChoreList>
{
    public async Task<Result<ChoreList>> HandleAsync(GetChoreListQuery query, CancellationToken cancellationToken = default)
    {
        ChoreList? choreList = await context.ChoreLists
            .AsNoTracking()
            .FirstOrDefaultAsync(cl => cl.Id == query.ChoreListId, cancellationToken);

        if (choreList is null)
        {
            return Result.Failure<ChoreList>(ChoreListErrors.NotFound);
        }

        return choreList;
    }
}