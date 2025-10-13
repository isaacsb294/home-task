using Domain.Users;
using Shared;

namespace Domain.Comments;

public class Comment : Entity
{
    internal Comment(string content,  Guid userId)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentNullException(nameof(content));
        }
        
        Id = Guid.CreateVersion7();
        Content = content;
        UserId = userId;
        PublishedOn = DateTime.UtcNow;
    }
    
    private Comment()
    {
    }

    public string Content { get; private set; } = string.Empty;
    public Guid UserId { get; init; }
    public DateTime PublishedOn { get; private set; }
    public DateTime EditedAt { get; private set; }

    public virtual User User { get; init; } = null!;

    public void EditComment(string content)
    {
        Content = content;
        EditedAt = DateTime.UtcNow;
    }
}