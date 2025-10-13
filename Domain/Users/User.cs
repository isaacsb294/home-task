using System.Net.Mail;
using Domain.ChoreLists;
using Domain.Chores;
using Domain.Comments;
using Shared;

namespace Domain.Users;

public class User : Entity
{
    private User()
    {
    }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public MailAddress Email { get; private set; } = null!;

    public virtual List<ChoreList> ChoreLists { get; init; } = [];
    public virtual List<Chore> TaggedChores { get; init; } = [];
    public virtual List<Comment> Comments { get; init; } = [];

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

        return user;
    }
}