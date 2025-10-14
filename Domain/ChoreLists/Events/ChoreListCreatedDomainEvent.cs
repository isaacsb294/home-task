using Shared;

namespace Domain.ChoreLists.Events;

public record ChoreListCreatedDomainEvent(Guid ChoreListId) : IDomainEvent;