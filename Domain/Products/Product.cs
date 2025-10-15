using Domain.Products.Events;
using Shared;

namespace Domain.Products;

public class Product : Entity
{
    private Product()
    {
    }
    public Guid ChoreId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Uri Link { get; private set; } = null!;
    public Price LastKnownPrice { get; private set; } = Price.Zero;
    public DateOnly LastKnownPriceDate { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    internal static Result<Product> Create(
        Guid choreId,
        string name,
        string link,
        Price lastKnownPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Product>(ProductErrors.BlankName);
        }

        if (string.IsNullOrWhiteSpace(link))
        {
            return Result.Failure<Product>(ProductErrors.BlankLink);
        }

        return new Product
        {
            Id = Guid.CreateVersion7(),
            ChoreId = choreId,
            Name = name,
            Link = new Uri(link),
            LastKnownPrice = lastKnownPrice
        };
    }

    public void UpdateLastKnownPrice(Price newPrice)
    {
        LastKnownPrice = newPrice;
        LastKnownPriceDate = DateOnly.FromDateTime(DateTime.UtcNow);

        Raise(new LastKnownPriceUpdatedDomainEvent(Id, newPrice));
    }
}