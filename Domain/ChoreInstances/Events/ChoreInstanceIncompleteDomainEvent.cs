using Shared;

namespace Domain.ChoreInstances.Events;

public record ChoreInstanceIncompleteDomainEvent(Guid ChoreInstanceId) : IDomainEvent;