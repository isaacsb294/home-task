using Shared;

namespace Domain.ChoreInstances.Events;

public record ChoreInstanceCommentRemovedDomainEvent(Guid ChoreInstanceId, Guid CommentId) : IDomainEvent;