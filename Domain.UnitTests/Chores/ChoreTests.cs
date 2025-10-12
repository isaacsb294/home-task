using Domain.Chores;
using Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace Domain.UnitTests.Chores;

public class ChoreTests : BaseTest
{
    private static Chore CreateTestChore()
    {
        return Chore.Create(
            ChoreData.Name,
            ChoreData.Description,
            ChoreData.Priority,
            ChoreData.Frequency,
            ChoreData.Category,
            ChoreData.DayOfWeek);
    }
    
    [Fact]
    public void Create_SetsPropertyValues()
    {
        Chore chore = CreateTestChore();

        chore.Name.Should().Be(ChoreData.Name);
        chore.Description.Should().Be(ChoreData.Description);
        chore.Priority.Should().Be(ChoreData.Priority);
        chore.Frequency.Should().Be(ChoreData.Frequency);
        chore.DayOfWeek.Should().Be(ChoreData.DayOfWeek);
    }

    [Fact]
    public void Create_AppliesCorrectDefaults_WhenNotProvided()
    {
        var chore = Chore.Create(
            ChoreData.Name,
            ChoreData.Description,
            null,
            null,
            null, 
            null);

        chore.Frequency.Should().Be(ChoreFrequency.Daily);
        chore.Priority.Should().Be(ChorePriority.Low);
    }

    [Fact]
    public void Create_Throws_WhenNameIsWhiteSpace()
    {
        try
        {
            Chore.Create(
                "",
                "",
                null,
                null,
                null,
                null);
        }
        catch (ArgumentException exception)
        {
            exception.ParamName.Should().Be("name");
        }
    }

    [Fact]
    public void Create_Throws_WhenDescriptionIsWhiteSpace()
    {
        try
        {
            Chore.Create(
                ChoreData.Name,
                "",
                null,
                null,
                null,
                null);
        }
        catch (ArgumentException exception)
        {
            exception.ParamName.Should().Be("description");
        }
    }
    
    [Fact]
    public void Create_RaisesDomainEvent()
    {
        Chore chore = CreateTestChore();
        
        var domainEvent = AssertDomainEventWasRaised<ChoreCreatedDomainEvent>(chore);
        
        domainEvent.Should().NotBeNull();
        domainEvent.ChoreId.Should().Be(chore.Id);
    }

    [Fact]
    public void MarkComplete_RaisesDomainEvent()
    {
        Chore chore = CreateTestChore();
        
        chore.MarkComplete();

        var domainEvent = AssertDomainEventWasRaised<ChoreCompletedDomainEvent>(chore);
        
        domainEvent.ChoreId.Should().Be(chore.Id);
    }

    [Fact]
    public void MarkComplete_DoesNotRaiseDomainEvent_WhenAlreadyComplete()
    {
        Chore chore = CreateTestChore();
        
        chore.MarkComplete();
        chore.MarkComplete();
        
        chore.DomainEvents
            .OfType<ChoreCompletedDomainEvent>()
            .Should()
            .HaveCount(1);
    }

    [Fact]
    public void MarkIncomplete_RaisesDomainEvent()
    {
        Chore chore = CreateTestChore();
        
        chore.MarkComplete();
        chore.MarkIncomplete();

        var domainEvent = AssertDomainEventWasRaised<ChoreIncompleteDomainEvent>(chore);
        
        domainEvent.ChoreId.Should().Be(chore.Id);
    }

    [Fact]
    public void MarkIncomplete_DoesNotRaiseDomainEvent_WhenAlreadyIncomplete()
    {
        Chore chore = CreateTestChore();
        
        chore.MarkIncomplete();
        
        chore.DomainEvents
            .OfType<ChoreIncompleteDomainEvent>()
            .Should()
            .BeEmpty();
    }
}