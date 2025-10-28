using Application.Abstractions.Messaging;

namespace Application.ChoreLists.EditChoreList;

public record EditChoreListCommand(
    Guid ChoreListId,
    string? Name,
    string? Description) : ICommand;