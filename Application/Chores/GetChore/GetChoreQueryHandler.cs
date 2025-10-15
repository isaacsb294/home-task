using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Chores;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Chores.GetChore;

public class GetChoreQueryHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext) : IQueryHandler<GetChoreQuery, Chore>
{
    public async Task<Result<Chore>> HandleAsync(
        GetChoreQuery query,
        CancellationToken cancellationToken = default)
    {
        Chore? chore = await dbContext.Chores
            .AsNoTracking()
            .Include(x => x.Products)
            .AsNoTracking()
            .Include(x => x.ResponsiblePersons)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == query.ChoreId,  cancellationToken);

        if (chore?.ResponsiblePersons.FirstOrDefault(rp => rp.Id == userContext.UserId) is null ||
            chore.UserId != userContext.UserId)
        {
            return Result.Failure<Chore>(ChoreErrors.NotFound);
        }

        return chore;
    }
}