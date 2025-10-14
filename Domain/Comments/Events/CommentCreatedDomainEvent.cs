using Shared;

namespace Domain.Comments.Events;

public record CommentCreatedDomainEvent(Guid CommentId) : IDomainEvent;