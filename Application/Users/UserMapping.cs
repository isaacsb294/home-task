using Domain.Users;

namespace Application.Users;

public static class UserMapping
{
    public static UserDto ToDto(this User user, bool includeEmail = false)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = includeEmail ? user.Email.ToString() : string.Empty
        };
    }
}