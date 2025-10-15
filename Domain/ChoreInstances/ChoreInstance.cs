using Domain.ChoreInstances.Events;
using Domain.Comments;
using Shared;

namespace Domain.ChoreInstances;

public class ChoreInstance : Entity
{
    private readonly List<Comment> _comments = [];

    public ChoreInstance(Guid choreId, DateOnly dateDue)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(
            dateDue,
            DateOnly.FromDateTime(DateTime.Now),
            nameof(dateDue));

        Id = Guid.CreateVersion7();
        ChoreId = choreId;
        DateDue = dateDue;
    }

    public Guid ChoreId { get; init; }
    public DateOnly DateDue { get; init; }
    public bool IsCompleted { get; private set; }
    public Uri[] ImageLinks { get; init; } = [];
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    public Result AddComment(Guid userId, string commentContent)
    {
        var comment = new Comment(userId, Id, commentContent);
        _comments.Add(comment);

        Raise(new ChoreInstanceCommentAddedDomainEvent(Id, comment.Id));

        return Result.Success();
    }

    public Result RemoveComment(Guid commentId)
    {
        Comment? toRemove = _comments.FirstOrDefault(c => c.Id == commentId);

        if (toRemove is null)
        {
            return Result.Failure(ChoreInstanceErrors.CommentNotExists);
        }

        _comments.RemoveAll(c => c.Id == commentId);

        Raise(new ChoreInstanceCommentRemovedDomainEvent(Id, commentId));

        return Result.Success();
    }

    public Result MarkComplete()
    {
        if (IsCompleted)
        {
            return Result.Failure(ChoreInstanceErrors.AlreadyComplete);
        }

        IsCompleted = true;
        Raise(new ChoreInstanceCompletedDomainEvent(Id));

        return Result.Success();
    }

    public Result MarkIncomplete()
    {
        if (!IsCompleted)
        {
            return Result.Failure(ChoreInstanceErrors.AlreadyIncomplete);
        }

        IsCompleted = false;
        Raise(new ChoreInstanceIncompleteDomainEvent(Id));

        return Result.Success();
    }
}