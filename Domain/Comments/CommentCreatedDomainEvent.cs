using Shared;

namespace Domain.Comments;

public record CommentCreatedDomainEvent(Guid CommentId) : IDomainEvent;