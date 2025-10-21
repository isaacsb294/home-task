namespace Application.Abstractions.Auth;

public interface IUserContext
{
    Guid UserId { get; }
    string IdentityId { get; }
}