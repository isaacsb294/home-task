using Shared;

namespace Domain.Chores;

public record ChoreCompletedDomainEvent(Guid ChoreId) : IDomainEvent;