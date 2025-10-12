using Shared;

namespace Domain.ChoreLists;

public record ChoreAddedToListDomainEvent(Guid ChoreListId, Guid ChoreId) : IDomainEvent;