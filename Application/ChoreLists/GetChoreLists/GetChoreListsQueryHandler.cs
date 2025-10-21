using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.ChoreLists.GetChoreLists;

public sealed class GetChoreListsQueryHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext) : IQueryHandler<GetChoreListsQuery, List<ChoreListDto>>
{
    public async Task<Result<List<ChoreListDto>>> HandleAsync(
        GetChoreListsQuery query, 
        CancellationToken cancellationToken)
    {
        List<ChoreListDto> choreListDtoList = await dbContext.ChoreLists
            .Where(cl => cl.UserId == userContext.UserId)
            .Select(cl => cl.ToDto())
            .ToListAsync(cancellationToken);

        return choreListDtoList;
    }
}