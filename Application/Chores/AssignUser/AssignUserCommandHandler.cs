using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Chores;
using Domain.ChoreUsers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Chores.AssignUser;

public class AssignUserCommandHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext) : ICommandHandler<AssignUserCommand>
{
    public async Task<Result> HandleAsync(AssignUserCommand command, CancellationToken cancellationToken)
    {
        Chore? chore = await dbContext.Chores
            .Include(c => c.Assignees)
            .FirstOrDefaultAsync(c => c.Id == command.ChoreId, cancellationToken);

        if (chore is null || chore.UserId != userContext.UserId)
        {
            return Result.Failure(ChoreErrors.NotFound);
        }
        
        User? user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userContext.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        Result result = chore.AssignUser(user);

        if (result.IsFailure)
        {
            return result;
        }

        var choreUser = new ChoreUser
        {
            ChoreId = chore.Id,
            UserId = user.Id
        };
        
        dbContext.ChoreUsers.Add(choreUser);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}