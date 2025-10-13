using Domain.ChoreInstances;
using Domain.Chores;
using Domain.UnitTests.Chores;
using Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace Domain.UnitTests.ChoreInstances;

public class ChoreInstanceTests : BaseTest
{
    
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
    public void MarkComplete_DoesNotRaiseDomainEvent_WhenAlreadyComplete()
    {
        Chore chore = CreateTestChore();
        var choreInstance = new ChoreInstance(chore.Id, DateOnly.FromDateTime(DateTime.Now));

        choreInstance.MarkComplete();
        choreInstance.MarkComplete();

        choreInstance.DomainEvents
            .OfType<ChoreInstanceCompletedDomainEvent>()
            .Should()
            .HaveCount(1);
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
}