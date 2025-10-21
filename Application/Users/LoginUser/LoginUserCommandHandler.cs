using Application.Abstractions.Auth;
using Application.Abstractions.Messaging;
using Domain.Users;
using Shared;

namespace Application.Users.LoginUser;

public sealed class LoginUserCommandHandler(
    IJwtService jwtService) : ICommandHandler<LoginUserCommand, AccessTokenResponse>
{
    public async Task<Result<AccessTokenResponse>> HandleAsync(
        LoginUserCommand command, 
        CancellationToken cancellationToken)
    {
        Result<AccessTokenResponse> result = await jwtService.GetAccessTokenAsync(
            command.Email,
            command.Password,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }

        return result.Value;
    }
}