using Application.Abstractions.Messaging;

namespace Application.ChoreLists.GetChoreList;

public record GetChoreListQuery(Guid ChoreListId) : IQuery;