using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Chores;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Chores.EditChore;

public class EditChoreCommandHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext) : ICommandHandler<EditChoreCommand>
{
    public async Task<Result> HandleAsync(EditChoreCommand command, CancellationToken cancellationToken = default)
    {
        Chore? chore = await dbContext.Chores
            .FirstOrDefaultAsync(c => c.Id == command.ChoreId, cancellationToken);

        if (chore is null || chore.UserId != userContext.UserId)
        {
            return Result.Failure(ChoreErrors.NotFound);
        }
        
        Result result = chore.EditChoreInformation(
                command.Name,
                command.Description,
                command.Priority,
                command.Frequency,
                command.Category,
                command.DayOfWeek);

        if (result.IsFailure)
        {
            return result;
        }
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}