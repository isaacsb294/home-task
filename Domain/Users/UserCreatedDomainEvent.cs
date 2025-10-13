using Shared;

namespace Domain.Users;

public record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;