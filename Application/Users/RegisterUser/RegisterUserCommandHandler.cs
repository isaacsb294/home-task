using System.Net.Mail;
using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Users;
using Shared;

namespace Application.Users.RegisterUser;

public class RegisterUserCommandHandler(
    IApplicationDbContext dbContext,
    IAuthenticationService authenticationService) : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = User.Create(
            command.FirstName,
            command.LastName,
            new MailAddress(command.Email));

        string identityId = await authenticationService.RegisterAsync(
            user,
            command.Password,
            cancellationToken);

        user.SetIdentityId(identityId);

        foreach (Role role in user.Roles)
        {
            dbContext.Roles.Attach(role);
        }

        dbContext.Users.Add(user);

        await dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}