using Shared;

namespace Domain.ChoreLists;

public record ChoreListCreatedDomainEvent(Guid ChoreListId) : IDomainEvent;