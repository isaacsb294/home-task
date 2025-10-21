using System.Net.Mail;
using Domain.Users.Events;
using Shared;

namespace Domain.Users;

public class User : Entity
{
    private readonly List<Role> _roles = [];
    
    private User()
    {
    }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public MailAddress Email { get; private set; } = null!;
    
    public string IdentityId { get; private set; } = string.Empty;

    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    public static User Create(
        string firstName,
        string lastName,
        MailAddress email)
    {
        var user = new User
        {
            Id = Guid.CreateVersion7(),
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        user.Raise(new UserCreatedDomainEvent(user.Id));
        
        user._roles.Add(Role.Registered);

        return user;
    }
    
    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}