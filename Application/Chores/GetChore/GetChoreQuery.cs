using Application.Abstractions.Messaging;

namespace Application.Chores.GetChore;

public record GetChoreQuery(Guid ChoreId) : IQuery;