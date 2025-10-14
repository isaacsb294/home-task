using Shared;

namespace Domain.Products.Events;

public record LastKnownPriceUpdatedDomainEvent(Guid ProductId, Price Price) : IDomainEvent;