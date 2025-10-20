namespace Infrastructure.Authentication;

public sealed class JwtAuthOptions
{
    public const string SectionName = "JwtAuth";

    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string Key { get; init; } = string.Empty;
    public int ExpirationInMinutes { get; init; }
    public int RefreshTokenExpirationDays { get; init; }
}