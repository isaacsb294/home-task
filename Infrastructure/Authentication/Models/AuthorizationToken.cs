using System.Text.Json.Serialization;

namespace Infrastructure.Authentication.Models;

internal sealed class AuthorizationToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = string.Empty;

    [JsonPropertyName("refresh_token")] 
    public string RefreshToken { get; init; } = string.Empty;
    
    [JsonPropertyName("refresh_expires_in")]
    public int RefreshTokenExpiresIn { get; init; }
}
