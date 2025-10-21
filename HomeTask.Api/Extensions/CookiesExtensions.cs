namespace HomeTask.Api.Extensions;

public static class CookiesExtensions
{
    public static void SetRefreshToken(
        this IResponseCookies cookies, 
        string refreshToken,
        int expiresSeconds)
    {
        cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.Now.AddSeconds(expiresSeconds),
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None
        });
    }
}