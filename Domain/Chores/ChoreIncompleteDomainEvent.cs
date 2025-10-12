using Shared;

namespace Domain.Chores;

public record ChoreIncompleteDomainEvent(Guid ChoreId) : IDomainEvent;