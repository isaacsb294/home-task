using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Chores;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Chores.DeleteChore;

public class DeleteChoreCommandHandler(
    IApplicationDbContext  dbContext,
    IUserContext userContext) : ICommandHandler<DeleteChoreCommand>
{
    public async Task<Result> HandleAsync(DeleteChoreCommand command, CancellationToken cancellationToken = default)
    {
        Chore? chore = await dbContext.Chores.FirstOrDefaultAsync(c => c.Id == command.ChoreId, cancellationToken);

        if (chore is null || chore.UserId != userContext.UserId)
        {
            return Result.Failure(ChoreErrors.NotFound);
        }
        
        dbContext.Chores.Remove(chore);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}