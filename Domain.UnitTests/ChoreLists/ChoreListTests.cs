using Domain.ChoreLists;
using Domain.Chores;
using Domain.UnitTests.Chores;
using Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace Domain.UnitTests.ChoreLists;

public class ChoreListTests : BaseTest
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
    public void Create_SetsParamsCorrectly()
    {
        var choreList = ChoreList.Create(
            ChoreListData.Name,
            ChoreListData.Description);
        
        choreList.Name.Should().Be(ChoreListData.Name);
        choreList.Description.Should().Be(ChoreListData.Description);
        choreList.Chores.Should().BeEmpty();
    }

    [Fact]
    public void Create_Throws_WhenNameIsWhiteSpace()
    {
        try
        {
            ChoreList.Create("", "");
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
            ChoreList.Create(ChoreListData.Name, "");
        }
        catch (ArgumentException exception)
        {
            exception.ParamName.Should().Be("description");
        }
    }

    [Fact]
    public void Create_RaisesDomainEvent()
    {
        var choreList = ChoreList.Create(
            ChoreListData.Name,
            ChoreListData.Description);
        
        var domainEvent = AssertDomainEventWasRaised<ChoreListCreatedDomainEvent>(choreList);
        domainEvent.ChoreListId.Should().Be(choreList.Id);
    }

    [Fact]
    public void AddChore_ChoreIsAdded_AndDomainEventRaised()
    {
        var choreList = ChoreList.Create(
            ChoreListData.Name,
            ChoreListData.Description);
        
        Chore chore = CreateTestChore();

        choreList.AddChore(chore);
        choreList.Chores.Should().Contain(chore);

        var domainEvent = AssertDomainEventWasRaised<ChoreAddedToListDomainEvent>(choreList);
        domainEvent.ChoreListId.Should().Be(choreList.Id);
        domainEvent.ChoreId.Should().Be(chore.Id);
    }

    [Fact]
    public void RemoveChore_ChoreIsRemoved_AndDomainEventRaised()
    {
        var choreList = ChoreList.Create(
            ChoreListData.Name,
            ChoreListData.Description);
        
        Chore chore = CreateTestChore();

        choreList.AddChore(chore);
        choreList.Chores.Should().Contain(chore);
        
        choreList.RemoveChore(chore);
        choreList.Chores.Should().BeEmpty();
        
        var domainEvent = AssertDomainEventWasRaised<ChoreRemovedFromListDomainEvent>(choreList);
        domainEvent.ChoreListId.Should().Be(choreList.Id);
        domainEvent.ChoreId.Should().Be(chore.Id);
    }
}