using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.ChoreLists;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.ChoreLists.EditChoreList;

public class EditChoreListCommandHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext) : ICommandHandler<EditChoreListCommand>
{
    public async Task<Result> HandleAsync(
        EditChoreListCommand command, 
        CancellationToken cancellationToken)
    {
        ChoreList? choreList = await dbContext.ChoreLists.FirstOrDefaultAsync(
            cl => cl.Id == command.ChoreListId, 
            cancellationToken);

        if (choreList is null || choreList.UserId != userContext.UserId)
        {
            return Result.Failure(ChoreListErrors.NotFound);
        }
        
        choreList.EditDetails(command.Name, command.Description);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}