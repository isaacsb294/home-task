namespace Application.Chores;

public sealed class ProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Link { get; init; } = string.Empty;
    public decimal PriceAmount { get; init; }
    public string PriceCurrencyCode { get; init; } = string.Empty;
};