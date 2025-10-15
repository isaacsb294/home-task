using Shared;

namespace Domain.Chores.Events;

public record ChoreProductAddedDomainEvent(Guid ChoreId, Guid ProductId) : IDomainEvent;