using Domain.ChoreInstances;
using Domain.ChoreInstances.Events;
using Domain.Chores;
using Domain.Comments;
using Domain.UnitTests.Comments;
using Domain.UnitTests.Infrastructure;
using FluentAssertions;
using Shared;

namespace Domain.UnitTests.ChoreInstances;

public class ChoreInstanceTests : BaseTest
{
    [Fact]
    public void AddComment_CreatesNewComment_WhenSuccessful()
    {
        Chore chore = CreateTestChore();
        var choreInstance = new ChoreInstance(
            chore.Id, DateOnly.FromDateTime(DateTime.Now));

        choreInstance.AddComment(
            CreateTestUser().Id,
            CommentData.Content);
        
        choreInstance.Comments.Count.Should().Be(1);
    }
    
    [Fact]
    public void AddComment_RaisesDomainEvent_WhenSuccessful()
    {
        Chore chore = CreateTestChore();
        var choreInstance = new ChoreInstance(
            chore.Id, DateOnly.FromDateTime(DateTime.Now));

        choreInstance.AddComment(
            CreateTestUser().Id,
            CommentData.Content);

        Comment comment = choreInstance.Comments.First();
        var domainEvent = AssertDomainEventWasRaised<ChoreInstanceCommentAddedDomainEvent>(choreInstance);
        
        domainEvent.ChoreInstanceId.Should().Be(choreInstance.Id);
        domainEvent.CommentId.Should().Be(comment.Id);
    }

    [Fact]
    public void RemoveComment_RemovesComment_WhenSuccessful()
    {
        Chore chore = CreateTestChore();
        var choreInstance = new ChoreInstance(
            chore.Id, DateOnly.FromDateTime(DateTime.Now));
        
        choreInstance.AddComment(
            CreateTestUser().Id,
            CommentData.Content);
        
        Comment comment = choreInstance.Comments.First();
        
        choreInstance.RemoveComment(comment.Id);
        
        choreInstance.Comments.Count.Should().Be(0);
    }

    [Fact]
    public void RemoveComment_RaisesDomainEvent_WhenSuccessful()
    {
        Chore chore = CreateTestChore();
        var choreInstance = new ChoreInstance(
            chore.Id, DateOnly.FromDateTime(DateTime.Now));
        
        choreInstance.AddComment(
            CreateTestUser().Id,
            CommentData.Content);
        
        Comment comment = choreInstance.Comments.First();
        
        choreInstance.RemoveComment(comment.Id);
        
        var domainEvent = AssertDomainEventWasRaised<ChoreInstanceCommentRemovedDomainEvent>(choreInstance);
        domainEvent.ChoreInstanceId.Should().Be(choreInstance.Id);
        domainEvent.CommentId.Should().Be(comment.Id);
    }

    [Fact]
    public void RemoveComment_ReturnsError_OnFailure()
    {
        Chore chore = CreateTestChore();
        var choreInstance = new ChoreInstance(
            chore.Id, DateOnly.FromDateTime(DateTime.Now));
        
        Result result = choreInstance.RemoveComment(Guid.CreateVersion7());
        
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(ChoreInstanceErrors.CommentNotExists.Code);
    }
    
    [Fact]
    public void MarkComplete_RaisesDomainEvent()
    {
        Chore chore = CreateTestChore();
        var choreInstance = new ChoreInstance(chore.Id, DateOnly.FromDateTime(DateTime.Now));
        
        choreInstance.MarkComplete();

        var domainEvent = AssertDomainEventWasRaised<ChoreInstanceCompletedDomainEvent>(choreInstance);

        domainEvent.ChoreInstanceId.Should().Be(choreInstance.Id);
    }

    [Fact]
    public void MarkComplete_ReturnsError_WhenAlreadyComplete()
    {
        Chore chore = CreateTestChore();
        var choreInstance = new ChoreInstance(chore.Id, DateOnly.FromDateTime(DateTime.Now));

        choreInstance.MarkComplete();
        Result result = choreInstance.MarkComplete();
        
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(ChoreInstanceErrors.AlreadyComplete.Code);
    }

    [Fact]
    public void MarkIncomplete_RaisesDomainEvent()
    {
        Chore chore = CreateTestChore();
        var choreInstance = new ChoreInstance(chore.Id, DateOnly.FromDateTime(DateTime.Now));

        choreInstance.MarkComplete();
        choreInstance.MarkIncomplete();

        var domainEvent = AssertDomainEventWasRaised<ChoreInstanceIncompleteDomainEvent>(choreInstance);

        domainEvent.ChoreInstanceId.Should().Be(choreInstance.Id);
    }

    [Fact]
    public void MarkIncomplete_DoesNotRaiseDomainEvent_WhenAlreadyIncomplete()
    {
        Chore chore = CreateTestChore();
        var choreInstance = new ChoreInstance(chore.Id, DateOnly.FromDateTime(DateTime.Now));

        choreInstance.MarkIncomplete();

        choreInstance.DomainEvents
            .OfType<ChoreInstanceIncompleteDomainEvent>()
            .Should()
            .BeEmpty();
    }

    [Fact]
    public void MarkIncomplete_ReturnsError_WhenAlreadyIncomplete()
    {
        Chore chore = CreateTestChore();
        var choreInstance = new ChoreInstance(chore.Id, DateOnly.FromDateTime(DateTime.Now));

        Result result = choreInstance.MarkIncomplete();
        
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(ChoreInstanceErrors.AlreadyIncomplete.Code);
    }
}