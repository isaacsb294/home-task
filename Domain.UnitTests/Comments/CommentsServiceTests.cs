using Domain.ChoreInstances;
using Domain.Comments;
using Domain.Comments.Events;
using Domain.UnitTests.Infrastructure;
using Domain.UnitTests.Users;
using Domain.Users;
using FluentAssertions;

namespace Domain.UnitTests.Comments;

public class CommentsServiceTests : BaseTest
{
    private static Comment CreateTestComment()
    {
        var choreInstance = new ChoreInstance(Guid.CreateVersion7(), DateOnly.FromDateTime(DateTime.Now));
        var user = User.Create(UserData.FirstName, UserData.LastName, UserData.EmailAddress);

        Comment comment = CommentsService.AddCommentToChoreInstance(
            choreInstance, 
            CommentData.Content,
            user);

        return comment;
    }
    
    [Fact]
    public void AddCommentToChoreInstance_CreatesComment()
    {
        Comment comment = CreateTestComment();
        comment.Content.Should().Be(CommentData.Content);
    }
    
    [Fact]
    public void AddCommentToChoreInstance_RaisesDomainEvent()
    {
        Comment comment = CreateTestComment();
        var domainEvent = AssertDomainEventWasRaised<CommentCreatedDomainEvent>(comment);
        
        domainEvent.CommentId.Should().Be(comment.Id);
    }
}