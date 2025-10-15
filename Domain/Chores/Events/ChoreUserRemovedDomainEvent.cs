using Shared;

namespace Domain.Chores.Events;

public record ChoreUserRemovedDomainEvent(Guid ChoreId, Guid UserId) : IDomainEvent;