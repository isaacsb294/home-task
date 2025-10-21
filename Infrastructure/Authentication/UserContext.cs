using Application.Abstractions.Auth;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication;

public sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User?
            .GetUserId() ??
        throw new ApplicationException("User Context is not available");

    public string IdentityId =>
        httpContextAccessor
            .HttpContext?
            .User?
            .GetIdentityId() ??
        throw new ApplicationException("User Context is not available");
}