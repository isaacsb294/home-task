using Domain.ChoreInstances;
using Domain.Users;

namespace Domain.Comments;

public static class CommentsService
{
    public static Comment AddCommentToChoreInstance(ChoreInstance choreInstance, string commentContent, User user)
    {
        var comment = new Comment(commentContent, user.Id);
        choreInstance.AddComment(comment);
        
        comment.Raise(new CommentCreatedDomainEvent(comment.Id));
        
        return comment;
    }
}