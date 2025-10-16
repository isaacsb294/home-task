using Domain.Chores;
using Domain.Chores.Events;
using Domain.Products;
using Domain.UnitTests.Infrastructure;
using Domain.Users;
using FluentAssertions;
using Shared;

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
        Result<Chore> result = Chore.Create(
            ChoreData.UserId,
            ChoreData.ChoreListId,
            ChoreData.Name,
            ChoreData.Description,
            null,
            null,
            null,
            null);

        Chore chore = result.Value;

        chore.Frequency.Should().Be(ChoreFrequency.Daily);
        chore.Priority.Should().Be(ChorePriority.Low);
    }

    [Fact]
    public void Create_Throws_WhenNameIsWhiteSpace()
    {
        Result result = Chore.Create(
            ChoreData.UserId,
            ChoreData.ChoreListId,
            "",
            "",
            null,
            null,
            null,
            null);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(ChoreErrors.BlankName.Code);
    }

    [Fact]
    public void Create_ReturnsError_WhenDescriptionIsWhiteSpace()
    {
        Result result = Chore.Create(
            ChoreData.UserId,
            ChoreData.ChoreListId,
            ChoreData.Name,
            "",
            null,
            null,
            null,
            null);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(ChoreErrors.BlankDescription.Code);
    }

    [Fact]
    public void Create_RaisesDomainEvent_WhenSuccessful()
    {
        Chore chore = CreateTestChore();

        var domainEvent = AssertDomainEventWasRaised<ChoreCreatedDomainEvent>(chore);
        domainEvent.ChoreId.Should().Be(chore.Id);
    }

    [Fact]
    public void AddProduct_CreatesProduct_WhenSuccessful()
    {
        Chore chore = CreateTestChore();

        Result<Product> result = chore.AddProduct(
            ProductData.Name,
            ProductData.Link,
            ProductData.LastKnownPrice);

        result.IsSuccess.Should().BeTrue();
        chore.Products.Count.Should().Be(1);
        result.Value.Name.Should().Be(ProductData.Name);
    }

    [Fact]
    public void AddProduct_RaisesDomainEvent_WhenSuccessful()
    {
        Chore chore = CreateTestChore();

        Result<Product> result = chore.AddProduct(
            ProductData.Name,
            ProductData.Link,
            ProductData.LastKnownPrice);

        Product product = result.Value;
        var domainEvent = AssertDomainEventWasRaised<ChoreProductAddedDomainEvent>(chore);

        domainEvent.ChoreId.Should().Be(chore.Id);
        domainEvent.ProductId.Should().Be(product.Id);
    }

    [Fact]
    public void AddProduct_ReturnsError_WhenNameIsWhiteSpace()
    {
        Chore chore = CreateTestChore();

        Result<Product> result = chore.AddProduct(
            "",
            ProductData.Link,
            ProductData.LastKnownPrice);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(ProductErrors.BlankName.Code);
    }

    [Fact]
    public void AddProduct_ReturnsError_WhenLinkIsWhiteSpace()
    {
        Chore chore = CreateTestChore();

        Result<Product> result = chore.AddProduct(
            ProductData.Name,
            "",
            ProductData.LastKnownPrice);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(ProductErrors.BlankLink.Code);
    }

    [Fact]
    public void RemoveProduct_RemovesProduct_WhenSuccessful()
    {
        Chore chore = CreateTestChore();

        Result<Product> result = chore.AddProduct(
            ProductData.Name,
            ProductData.Link,
            ProductData.LastKnownPrice);

        Product product = result.Value;
        Result removeResult = chore.RemoveProduct(product.Id);

        removeResult.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void RemoveProduct_RaisesDomainEvent_WhenSuccessful()
    {
        Chore chore = CreateTestChore();

        Result<Product> result = chore.AddProduct(
            ProductData.Name,
            ProductData.Link,
            ProductData.LastKnownPrice);

        Product product = result.Value;
        chore.RemoveProduct(product.Id);

        var domainEvent = AssertDomainEventWasRaised<ChoreProductRemovedDomainEvent>(chore);

        domainEvent.ChoreId.Should().Be(chore.Id);
        domainEvent.ProductId.Should().Be(product.Id);
    }

    [Fact]
    public void RemoveProduct_ReturnsError_WhenNotFound()
    {
        Chore chore = CreateTestChore();
        Result result = chore.RemoveProduct(Guid.CreateVersion7());

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(ChoreErrors.ProductNotExists.Code);
    }

    [Fact]
    public void AssignUser_CreatesResponsiblePerson_WhenSuccessful()
    {
        Chore chore = CreateTestChore();
        User user = CreateTestUser();

        Result result = chore.AssignUser(user);

        result.IsSuccess.Should().BeTrue();
        chore.Assignees.Count.Should().Be(1);
        chore.Assignees.First().Id.Should().Be(user.Id);
    }

    [Fact]
    public void AssignUser_RaisesDomainEvent_WhenSuccessful()
    {
        Chore chore = CreateTestChore();
        User user = CreateTestUser();

        chore.AssignUser(user);

        var domainEvent = AssertDomainEventWasRaised<ChoreUserAddedDomainEvent>(chore);

        domainEvent.ChoreId.Should().Be(chore.Id);
        domainEvent.UserId.Should().Be(user.Id);
    }

    [Fact]
    public void AssignUser_ReturnsError_WhenAlreadyExists()
    {
        Chore chore = CreateTestChore();
        User user = CreateTestUser();

        chore.AssignUser(user);
        Result result = chore.AssignUser(user);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(ChoreErrors.UserExists.Code);
    }

    [Fact]
    public void UnassignUser_RemovesResponsiblePerson_WhenSuccessful()
    {
        Chore chore = CreateTestChore();
        User user = CreateTestUser();

        chore.AssignUser(user);

        Result result = chore.UnassignUser(user.Id);

        result.IsSuccess.Should().BeTrue();
        chore.Assignees.Count.Should().Be(0);
    }

    [Fact]
    public void UnassignUser_RaisesDomainEvent_WhenSuccessful()
    {
        Chore chore = CreateTestChore();
        User user = CreateTestUser();

        chore.AssignUser(user);
        chore.UnassignUser(user.Id);

        var domainEvent = AssertDomainEventWasRaised<ChoreUserRemovedDomainEvent>(chore);

        domainEvent.ChoreId.Should().Be(chore.Id);
        domainEvent.UserId.Should().Be(user.Id);
    }

    [Fact]
    public void UnassignUser_ReturnsError_WhenNotFound()
    {
        Chore chore = CreateTestChore();
        Result result = chore.UnassignUser(Guid.CreateVersion7());

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(ChoreErrors.UserNotExists.Code);
    }
}