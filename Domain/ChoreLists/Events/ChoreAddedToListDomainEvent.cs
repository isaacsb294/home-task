using Shared;

namespace Domain.ChoreLists.Events;

public record ChoreAddedToListDomainEvent(Guid ChoreListId, Guid ChoreId) : IDomainEvent;