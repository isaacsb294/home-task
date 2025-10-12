using Shared;

namespace Domain.Chores;

public sealed record ChoreCreatedDomainEvent(Guid ChoreId) : IDomainEvent;