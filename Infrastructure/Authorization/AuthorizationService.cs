using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Infrastructure.Authorization;

internal sealed class AuthorizationService(ApplicationDbContext dbContext)
{
    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        UserRolesResponse? response = await dbContext.Users
            .Where(user => user.IdentityId == identityId)
            .Select(user => new UserRolesResponse
            {
                Id = user.Id,
                Roles = user.Roles.ToList()
            })
            .FirstOrDefaultAsync();

        if (response is null)
        {
            throw new ApplicationException("User not found");
        }

        return response;
    }
}