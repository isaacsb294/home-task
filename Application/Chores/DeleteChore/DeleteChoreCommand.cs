using Application.Abstractions.Messaging;

namespace Application.Chores.DeleteChore;

public record DeleteChoreCommand(Guid ChoreId) : ICommand;