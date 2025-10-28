using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.ChoreLists;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.ChoreLists.DeleteChoreList;

public class DeleteChoreListCommandHandler(
    IApplicationDbContext dbContext, 
    IUserContext userContext) : ICommandHandler<DeleteChoreListCommand>
{
    public async Task<Result> HandleAsync(DeleteChoreListCommand command, CancellationToken cancellationToken = default)
    {
        ChoreList? choreList = await dbContext.ChoreLists.FirstOrDefaultAsync(
            cl => cl.Id == command.ChoreListId, 
            cancellationToken);

        if (choreList is null || choreList.UserId != userContext.UserId)
        {
            return Result.Failure(ChoreListErrors.NotFound);
        }

        dbContext.ChoreLists.Remove(choreList);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}