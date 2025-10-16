using Application.Abstractions.Messaging;

namespace Application.Chores.AddProduct;

public record AddProductCommand(
    Guid ChoreId,
    string Name,
    string Link,
    decimal PriceAmount,
    string PriceCurrencyCode) : ICommand;