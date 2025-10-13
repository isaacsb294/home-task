using System.Net.Mail;

namespace Domain.UnitTests.Users;

public sealed class UserData
{
    public const string FirstName = "FirstName";
    public const string LastName = "LastName";
    public static MailAddress EmailAddress = new("test@test.com");
}