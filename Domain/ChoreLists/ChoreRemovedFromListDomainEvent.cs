using Shared;

namespace Domain.ChoreLists;

public record ChoreRemovedFromListDomainEvent(Guid ChoreListId, Guid ChoreId) : IDomainEvent;