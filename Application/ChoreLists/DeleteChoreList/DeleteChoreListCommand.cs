using Application.Abstractions.Messaging;

namespace Application.ChoreLists.DeleteChoreList;

public record DeleteChoreListCommand(Guid ChoreListId) : ICommand;