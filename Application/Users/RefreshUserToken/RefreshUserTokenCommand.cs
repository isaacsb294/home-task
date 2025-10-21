using Application.Abstractions.Messaging;

namespace Application.Users.RefreshUserToken;

public record RefreshUserTokenCommand(string RefreshToken) : ICommand;