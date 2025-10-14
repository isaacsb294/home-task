using Domain.Products.Events;
using Shared;

namespace Domain.Products;

public class Product : Entity
{
    private Product()
    {
    }

    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Uri Link { get; private set; } = null!;
    public Price LastKnownPrice { get; private set; } = Price.Zero;
    public DateOnly LastKnownPriceDate { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public static Product Create(
        Guid userId,
        string name,
        string link,
        Price lastKnownPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(link))
        {
            throw new ArgumentNullException(nameof(link));
        }

        return new Product
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
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