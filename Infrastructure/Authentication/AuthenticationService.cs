using System.Net.Http.Json;
using Application.Abstractions.Auth;
using Domain.Users;
using Infrastructure.Authentication.Models;

namespace Infrastructure.Authentication;

public sealed class AuthenticationService(HttpClient httpClient) : IAuthenticationService
{
    private const string PasswordCredentialType = "password";
    public async Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken)
    {
        UserRepresentationModel userRepresentationModel = UserRepresentationModel.FromUser(user);

        userRepresentationModel.Credentials =
        [
            new CredentialRepresentationModel
            {
                Value = password,
                Temporary = false,
                Type = PasswordCredentialType
            }
        ];
        
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(
            "users",
            userRepresentationModel,
            cancellationToken);

        return ExtractIdentityId(response);
    }

    private static string ExtractIdentityId(HttpResponseMessage response)
    {
        const string usersSegmentName = "users/";
        
        string? locationHeader = response.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header is null");
        }

        int userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName,
            StringComparison.InvariantCultureIgnoreCase);
        
        string userIdentityId = locationHeader[(userSegmentValueIndex + usersSegmentName.Length)..];
        
        return userIdentityId;
    }
}