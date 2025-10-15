using Shared;

namespace Domain.Chores.Events;

public record ChoreUserAddedDomainEvent(Guid ChoreId, Guid UserId) : IDomainEvent;