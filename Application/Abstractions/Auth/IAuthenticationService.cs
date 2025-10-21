using Domain.Users;

namespace Application.Abstractions.Auth;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken);
}