using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Users.GetUser;

public class GetUserQueryHandler(
    IApplicationDbContext dbContext) : IQueryHandler<GetUserQuery, User>
{
    public async Task<Result<User>> HandleAsync(GetUserQuery query, CancellationToken cancellationToken = default)
    {
        User? user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == query.UserId, cancellationToken);
 
        if (user is null)
        {
            return Result.Failure<User>(UserErrors.NotFound);
        }

        return user;
    }
}