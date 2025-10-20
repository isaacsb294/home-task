using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Chores;
using Domain.ChoreUsers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Chores.UnassignUser;

public class UnassignUserCommandHandler(
    IApplicationDbContext dbContext) : ICommandHandler<UnassignUserCommand>
{
    public async Task<Result> HandleAsync(UnassignUserCommand command, CancellationToken cancellationToken = default)
    {
        Chore? chore = await dbContext.Chores
            .Include(c => c.Assignees)
            .FirstOrDefaultAsync(chore => chore.Id == command.ChoreId, cancellationToken);

        if (chore is null)
        {
            return Result.Failure(ChoreErrors.NotFound);
        }

        User? user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        Result result = chore.UnassignUser(user.Id);

        if (result.IsFailure)
        {
            return result;
        }

        ChoreUser? choreUser = await dbContext.ChoreUsers
            .FirstOrDefaultAsync(cu => cu.UserId == user.Id && cu.ChoreId == chore.Id, cancellationToken);

        if (choreUser is null)
        {
            return Result.Failure(Error.NotFound("The relationship between chore and user could not be found"));
        }

        dbContext.ChoreUsers.Remove(choreUser);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}