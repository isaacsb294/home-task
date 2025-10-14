using System.Net.Mail;

namespace Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(MailAddress to, string subject, string body);
}