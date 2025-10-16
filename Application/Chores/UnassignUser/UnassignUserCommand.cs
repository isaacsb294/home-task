using Application.Abstractions.Messaging;

namespace Application.Chores.UnassignUser;

public record UnassignUserCommand(Guid ChoreId, Guid UserId) : ICommand;