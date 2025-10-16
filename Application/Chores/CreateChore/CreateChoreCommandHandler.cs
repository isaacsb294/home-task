using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.ChoreLists;
using Domain.Chores;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Chores.CreateChore;

public class CreateChoreCommandHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext) : ICommandHandler<CreateChoreCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(CreateChoreCommand command,
        CancellationToken cancellationToken = default)
    {
        User? user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user is null || user.Id != userContext.UserId)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        ChoreList? choreList =
            await dbContext.ChoreLists
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.UserId == user.Id, cancellationToken);

        if (choreList is null)
        {
            return Result.Failure<Guid>(ChoreListErrors.NotFound);
        }

        Result<Chore> result = Chore.Create(
            command.UserId,
            command.ChoreListId,
            command.Name,
            command.Description,
            command.Priority,
            command.Frequency,
            command.Category,
            command.DayOfWeek);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        dbContext.Chores.Add(result.Value);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return result.Value.Id;
    }
}