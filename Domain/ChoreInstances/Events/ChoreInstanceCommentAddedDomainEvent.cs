using Shared;

namespace Domain.ChoreInstances.Events;

public record ChoreInstanceCommentAddedDomainEvent(Guid ChoreInstanceId, Guid CommentId) : IDomainEvent;