using Shared;

namespace Domain.Chores.Events;

public sealed record ChoreCreatedDomainEvent(Guid ChoreId) : IDomainEvent;