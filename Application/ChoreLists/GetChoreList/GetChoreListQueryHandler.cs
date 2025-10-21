using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.ChoreLists;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.ChoreLists.GetChoreList;

public class GetChoreListQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetChoreListQuery, ChoreListDto>
{
    public async Task<Result<ChoreListDto>> HandleAsync(GetChoreListQuery query,
        CancellationToken cancellationToken = default)
    {
        ChoreListDto? choreListDto = await context.ChoreLists
            .AsNoTracking()
            .Select(choreList => choreList.ToDto())
            .FirstOrDefaultAsync(cl => cl.Id == query.ChoreListId, cancellationToken);

        if (choreListDto is null)
        {
            return Result.Failure<ChoreListDto>(ChoreListErrors.NotFound);
        }

        return choreListDto;
    }
}