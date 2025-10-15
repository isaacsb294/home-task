using Shared;

namespace Domain.Comments;

public class Comment : Entity
{
    internal Comment(Guid userId, Guid choreInstanceId, string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentNullException(nameof(content));
        }
        
        Id = Guid.CreateVersion7();
        Content = content;
        UserId = userId;
        ChoreInstanceId = choreInstanceId;
        PublishedOn = DateTime.UtcNow;
    }
    
    public Guid UserId { get; init; }
    public Guid ChoreInstanceId { get; init; }
    public string Content { get; private set; }
    public DateTime PublishedOn { get; private set; }
    public DateTime EditedAt { get; private set; }

    public void EditComment(string content)
    {
        Content = content;
        EditedAt = DateTime.UtcNow;
    }
}