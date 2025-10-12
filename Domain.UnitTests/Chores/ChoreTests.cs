using Domain.Chores;
using Domain.Products;
using Domain.UnitTests.Infrastructure;
using Domain.UnitTests.Products;
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
    public void Create_AssignsPropertyValues()
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

    [Fact]
    public void AddProduct_AddsProduct()
    {
        Chore chore = CreateTestChore();
        var product = Product.Create(
            ProductData.Name,
            ProductData.Link,
            ProductData.Price);

        chore.AddProduct(product);

        chore.Products.FirstOrDefault(p => p.Id == product.Id).Should().NotBeNull();
    }

    [Fact]
    public void RemoveProduct_RemovesProduct()
    {
        Chore chore = CreateTestChore();
        var product = Product.Create(
            ProductData.Name,
            ProductData.Link,
            ProductData.Price);

        chore.AddProduct(product);
        chore.RemoveProduct(product);

        chore.Products.Should().BeEmpty();
    }

    [Fact]
    public void RemoveProduct_Throws_WhenProductDoesntExist()
    {
        Chore chore = CreateTestChore();
        var product = Product.Create(
            ProductData.Name,
            ProductData.Link,
            ProductData.Price);

        try
        {
            chore.RemoveProduct(product);
        }
        catch (InvalidOperationException exception)
        {
            exception.Message.Should().Be("Product not found");
        }
    }
}