using Application.Abstractions.Messaging;

namespace Application.Chores.AssignUser;

public record AssignUserCommand(Guid ChoreId, Guid UserId) : ICommand;