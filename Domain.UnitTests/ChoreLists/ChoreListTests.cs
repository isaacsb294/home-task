using Domain.ChoreLists;
using Domain.Chores;
using Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace Domain.UnitTests.ChoreLists;

public class ChoreListTests : BaseTest
{
    [Fact]
    public void Create_AssignsParamsCorrectly()
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
            ChoreList.Create(ChoreListData.Name, "");
        }
        catch (ArgumentNullException exception)
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
    public void AddChore_ChoreIsAdded()
    {
        var choreList = ChoreList.Create(
            ChoreListData.Name,
            ChoreListData.Description);

        Chore chore = CreateTestChore();

        choreList.AddChore(chore);
        choreList.Chores.Should().Contain(chore);
    }

    [Fact]
    public void AddChore_RaisesDomainEvent()
    {
        var choreList = ChoreList.Create(
            ChoreListData.Name,
            ChoreListData.Description);

        Chore chore = CreateTestChore();

        choreList.AddChore(chore);

        var domainEvent = AssertDomainEventWasRaised<ChoreAddedToListDomainEvent>(choreList);
        domainEvent.ChoreListId.Should().Be(choreList.Id);
        domainEvent.ChoreId.Should().Be(chore.Id);
    }

    [Fact]
    public void RemoveChore_ChoreIsRemoved()
    {
        var choreList = ChoreList.Create(
            ChoreListData.Name,
            ChoreListData.Description);

        Chore chore = CreateTestChore();

        choreList.AddChore(chore);
        choreList.Chores.Should().Contain(chore);

        choreList.RemoveChore(chore);
        choreList.Chores.Should().BeEmpty();
    }

    [Fact]
    public void RemoveChore_RaisesDomainEvent()
    {
        var choreList = ChoreList.Create(
            ChoreListData.Name,
            ChoreListData.Description);

        Chore chore = CreateTestChore();

        choreList.AddChore(chore);
        choreList.Chores.Should().Contain(chore);

        choreList.RemoveChore(chore);

        var domainEvent = AssertDomainEventWasRaised<ChoreRemovedFromListDomainEvent>(choreList);
        domainEvent.ChoreListId.Should().Be(choreList.Id);
        domainEvent.ChoreId.Should().Be(chore.Id);
    }
}