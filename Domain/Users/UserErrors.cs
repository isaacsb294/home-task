using Shared;

namespace Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound",
        "User not found");
}