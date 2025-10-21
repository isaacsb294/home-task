using System.Net.Http.Json;
using Application.Abstractions.Auth;
using Application.Users.LoginUser;
using Infrastructure.Authentication.Models;
using Microsoft.Extensions.Options;
using Shared;

namespace Infrastructure.Authentication;

public class JwtService(
    HttpClient httpClient,
    IOptions<KeycloakOptions> keycloakOptions) : IJwtService
{
    private static readonly Error AuthenticationFailed = new(
        "Keycloak.AuthenticationFailed",
        "Failed to acquire access token due to an authentication failure.");

    private readonly KeycloakOptions _keycloakOptions = keycloakOptions.Value;

    public async Task<Result<AccessTokenResponse>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        KeyValuePair<string, string>[] requestFormParams =
        [
            new("client_id", _keycloakOptions.AuthClientId),
            new("client_secret", _keycloakOptions.AuthClientSecret),
            new("scope", "openid email"),
            new("grant_type", "password"),
            new("username", email),
            new("password", password)
        ];

        return await RequestTokenAsync(requestFormParams, cancellationToken);
    }

    public async Task<Result<AccessTokenResponse>> RefreshAccessTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        KeyValuePair<string, string>[] requestFormParams =
        [
            new("client_id", _keycloakOptions.AuthClientId),
            new("client_secret", _keycloakOptions.AuthClientSecret),
            new("scope", "openid email"),
            new("grant_type", "refresh_token"),
            new("refresh_token", refreshToken)
        ];

        return await RequestTokenAsync(requestFormParams, cancellationToken);
    }

    private async Task<Result<AccessTokenResponse>> RequestTokenAsync(
        KeyValuePair<string, string>[] requestFormParams,
        CancellationToken cancellationToken)
    {
        try
        {
            var requestContent = new FormUrlEncodedContent(requestFormParams);

            HttpResponseMessage response = await httpClient.PostAsync(
                "",
                requestContent,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

            if (authorizationToken is null)
            {
                return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
            }

            return new AccessTokenResponse(
                authorizationToken.AccessToken, 
                authorizationToken.RefreshToken,
                authorizationToken.RefreshTokenExpiresIn);
        }
        catch (HttpRequestException)
        {
            return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
        }
    }
}