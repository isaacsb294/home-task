using Shared;

namespace Domain.ChoreInstances;

public record ChoreInstanceCompletedDomainEvent(Guid ChoreInstanceId) : IDomainEvent;