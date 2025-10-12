using Shared;

namespace Domain.UnitTests.Products;

public sealed class ProductData
{
    public const string Name = "Test Product";
    public const string Link = "https://www.test.com/";
    public static readonly Price Price = new Price(20.50m, Currency.Gbp);
}