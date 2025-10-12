using Shared;

namespace Domain.Products;

public record LastKnownPriceUpdatedDomainEvent(Guid ProductId, Price Price) : IDomainEvent;