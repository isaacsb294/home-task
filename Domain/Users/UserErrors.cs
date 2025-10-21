using Shared;

namespace Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound",
        "User not found");
    
    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Information entered is not valid");
    
    public static readonly Error InvalidRefreshToken = new(
        "User.InvalidRefreshToken",
        "User session has expired, please log in again.");
}