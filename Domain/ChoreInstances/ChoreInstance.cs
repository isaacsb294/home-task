using Domain.ChoreInstances.Events;
using Domain.Chores;
using Domain.Comments;
using Shared;

namespace Domain.ChoreInstances;

public class ChoreInstance : Entity
{
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
    private List<Comment> _comments { get; init; } = [];
    public IReadOnlyCollection<Comment> Comments => _comments;
    public bool IsCompleted { get; private set; }
    public Uri[] ImageLinks { get; init; } = [];

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public void RemoveComment(Comment comment)
    {
        _comments.RemoveAll(c => c.Id == comment.Id);
    }
    
    public void MarkComplete()
    {
        if (IsCompleted)
        {
            return;
        }

        IsCompleted = true;
        Raise(new ChoreInstanceCompletedDomainEvent(Id));
    }

    public void MarkIncomplete()
    {
        if (!IsCompleted)
        {
            return;
        }

        IsCompleted = false;
        Raise(new ChoreInstanceIncompleteDomainEvent(Id));
    }
}