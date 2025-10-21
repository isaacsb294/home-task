using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Users.GetSelf;

public sealed class GetSelfQueryHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext) : IQueryHandler<GetSelfQuery, UserDto>
{
    public async Task<Result<UserDto>> HandleAsync(
        GetSelfQuery query, 
        CancellationToken cancellationToken )
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(
            u => u.Id == userContext.UserId,
            cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto>(UserErrors.NotFound);
        }

        return user.ToDto(true);
    }
}