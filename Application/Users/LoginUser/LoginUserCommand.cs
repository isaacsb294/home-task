using Application.Abstractions.Messaging;

namespace Application.Users.LoginUser;

public record LoginUserCommand(string Email, string Password) : ICommand;