using Domain.Chores;
using Domain.Products;
using Domain.UnitTests.Infrastructure;
using Domain.UnitTests.Products;
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
        domainEvent.ChoreId.Should().Be(chore.Id);
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