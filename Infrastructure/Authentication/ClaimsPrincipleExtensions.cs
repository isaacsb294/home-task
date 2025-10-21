using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Authentication;

public static class ClaimsPrincipleExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        string? userId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
        
        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId : 
            throw new ApplicationException("Invalid user id");
    }

    public static string GetIdentityId(this ClaimsPrincipal principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier) ??
               throw new ApplicationException("Invalid identity id");
    }
}