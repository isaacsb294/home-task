using Shared;

namespace Domain.Chores.Events;

public record ChoreProductRemovedDomainEvent(Guid ChoreId, Guid ProductId) : IDomainEvent;