using Application.Abstractions.Messaging;

namespace Application.ChoreLists.CreateChoreList;

public record CreateChoreListCommand(string Name, string Description) : ICommand;