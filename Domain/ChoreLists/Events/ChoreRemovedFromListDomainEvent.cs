using Shared;

namespace Domain.ChoreLists.Events;

public record ChoreRemovedFromListDomainEvent(Guid ChoreListId, Guid ChoreId) : IDomainEvent;