using Domain.Products;
using Domain.Products.Events;
using Domain.UnitTests.Infrastructure;
using FluentAssertions;
using Shared;

namespace Domain.UnitTests.Products;

public class ProductTests : BaseTest
{
    [Fact]
    public void Create_AssignsParamsCorrectly()
    {
        var product = Product.Create(
            CreateTestUser().Id,
            ProductData.Name,
            ProductData.Link,
            ProductData.Price);
        
        product.Name.Should().Be(ProductData.Name);
        product.Link.AbsoluteUri.Should().Be(ProductData.Link);
        product.LastKnownPrice.Should().Be(ProductData.Price);
    }

    [Fact]
    public void Create_Throws_WhenNameIsWhiteSpace()
    {
        try
        {
            Product.Create(
                CreateTestUser().Id,
                "",
                "",
                Price.Zero);
        }
        catch (ArgumentNullException exception)
        {
            exception.ParamName.Should().Be("name");
        }
    }

    [Fact]
    public void Create_Throws_WhenLinkIsWhiteSpace()
    {
        try
        {
            Product.Create(
                CreateTestUser().Id,
                ProductData.Name,
                "",
                Price.Zero);
        }
        catch (ArgumentNullException exception)
        {
            exception.ParamName.Should().Be("link");
        }
    }

    [Fact]
    public void UpdateLastKnownPrice_ShouldRaiseDomainEvent()
    {
        var product = Product.Create(
            CreateTestUser().Id,
            ProductData.Name,
            ProductData.Link,
            ProductData.Price);

        var newPrice = new Price(50.0m, Currency.Gbp);
        
        product.UpdateLastKnownPrice(newPrice);
        
        product.LastKnownPrice.Should().Be(newPrice);
        
        var domainEvent = AssertDomainEventWasRaised<LastKnownPriceUpdatedDomainEvent>(product);
        
        domainEvent.ProductId.Should().Be(product.Id);
        domainEvent.Price.Should().Be(newPrice);
    }
}