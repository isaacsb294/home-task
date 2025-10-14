using Shared;

namespace Domain.ChoreInstances.Events;

public record ChoreInstanceCompletedDomainEvent(Guid ChoreInstanceId) : IDomainEvent;