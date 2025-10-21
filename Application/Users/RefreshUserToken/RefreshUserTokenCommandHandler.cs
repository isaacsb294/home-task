using Application.Abstractions.Auth;
using Application.Abstractions.Messaging;
using Application.Users.LoginUser;
using Domain.Users;
using Shared;

namespace Application.Users.RefreshUserToken;

public sealed class RefreshUserTokenCommandHandler(
    IJwtService jwtService) : ICommandHandler<RefreshUserTokenCommand, AccessTokenResponse>
{
    public async Task<Result<AccessTokenResponse>> HandleAsync(
        RefreshUserTokenCommand command, 
        CancellationToken cancellationToken)
    {
        Result<AccessTokenResponse> result = await jwtService.RefreshAccessTokenAsync(
            command.RefreshToken,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidRefreshToken);
        }

        return result.Value;
    }
}