using Shared;

namespace Domain.ChoreInstances;

public record ChoreInstanceIncompleteDomainEvent(Guid ChoreInstanceId) : IDomainEvent;