using Domain.Chores;
using Domain.Chores.Events;
using Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace Domain.UnitTests.Chores;

public class ChoreTests : BaseTest
{
    [Fact]
    public void Create_AssignsPropertyValues()
    {
        Chore chore = CreateTestChore();

        chore.Name.Should().Be(ChoreData.Name);
        chore.Description.Should().Be(ChoreData.Description);
        chore.Priority.Should().Be(ChoreData.Priority);
        chore.Category.Should().Be(ChoreData.Category);
        chore.Frequency.Should().Be(ChoreData.Frequency);
        chore.DayOfWeek.Should().Be(ChoreData.DayOfWeek);
    }

    [Fact]
    public void Create_AppliesCorrectDefaults_WhenNotProvided()
    {
        var chore = Chore.Create(
            ChoreData.UserId,
            ChoreData.ChoreListId,
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
                ChoreData.UserId,
                ChoreData.ChoreListId,
                "",
                "",
                null,
                null,
                null,
                null);
        }
        catch (ArgumentNullException exception)
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
                ChoreData.UserId,
                ChoreData.ChoreListId,
                ChoreData.Name,
                "",
                null,
                null,
                null,
                null);
        }
        catch (ArgumentNullException exception)
        {
            exception.ParamName.Should().Be("description");
        }
    }

    [Fact]
    public void Create_RaisesDomainEvent()
    {
        Chore chore = CreateTestChore();

        var domainEvent = AssertDomainEventWasRaised<ChoreCreatedDomainEvent>(chore);
        domainEvent.ChoreId.Should().Be(chore.Id);
    }
}