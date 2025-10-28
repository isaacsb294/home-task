using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Chores;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Chores.GetChore;

public class GetChoreQueryHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext) : IQueryHandler<GetChoreQuery, ChoreDto>
{
    public async Task<Result<ChoreDto>> HandleAsync(
        GetChoreQuery query,
        CancellationToken cancellationToken = default)
    {
        Chore? chore = await dbContext.Chores
            .AsNoTracking()
            .Include(x => x.Products)
            .AsNoTracking()
            .Include(x => x.Assignees)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == query.ChoreId,  cancellationToken);

        if (chore?.UserId != userContext.UserId || 
            chore.UserId != userContext.UserId &&
            chore.Assignees.FirstOrDefault(rp => rp.Id == userContext.UserId) is null)
        {
            return Result.Failure<ChoreDto>(ChoreErrors.NotFound);
        }

        return chore.ToDto();
    }
}