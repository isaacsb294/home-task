using Shared;

namespace Domain.Users.Events;

public record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;