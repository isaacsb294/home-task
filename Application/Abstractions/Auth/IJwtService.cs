using Application.Users.LoginUser;
using Shared;

namespace Application.Abstractions.Auth;

public interface IJwtService
{
    Task<Result<AccessTokenResponse>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
    
    Task<Result<AccessTokenResponse>> RefreshAccessTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);
}