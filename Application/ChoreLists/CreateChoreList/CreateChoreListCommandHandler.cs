using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.ChoreLists;
using Shared;

namespace Application.ChoreLists.CreateChoreList;

public sealed class CreateChoreListCommandHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext) : ICommandHandler<CreateChoreListCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(
        CreateChoreListCommand command, 
        CancellationToken cancellationToken)
    {
        var choreList = ChoreList.Create(
            userContext.UserId,
            command.Name,
            command.Description);

        dbContext.ChoreLists.Add(choreList);

        await dbContext.SaveChangesAsync(cancellationToken);

        return choreList.Id;
    }
}