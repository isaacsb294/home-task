using Domain.ChoreLists;
using Domain.ChoreLists.Events;
using Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace Domain.UnitTests.ChoreLists;

public class ChoreListTests : BaseTest
{
    [Fact]
    public void Create_AssignsParamsCorrectly()
    {
        var choreList = ChoreList.Create(
            CreateTestUser().Id,
            ChoreListData.Name,
            ChoreListData.Description);

        choreList.Name.Should().Be(ChoreListData.Name);
        choreList.Description.Should().Be(ChoreListData.Description);
    }

    [Fact]
    public void Create_Throws_WhenNameIsWhiteSpace()
    {
        try
        {
            ChoreList.Create(CreateTestUser().Id, "", "");
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
            ChoreList.Create(CreateTestUser().Id, ChoreListData.Name, "");
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
            CreateTestUser().Id,
            ChoreListData.Name,
            ChoreListData.Description);

        var domainEvent = AssertDomainEventWasRaised<ChoreListCreatedDomainEvent>(choreList);
        domainEvent.ChoreListId.Should().Be(choreList.Id);
    }
}