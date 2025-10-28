namespace HomeTask.Api.Controllers.Chores;

public record AddProductRequest(
    string Name,
    string Link,
    decimal PriceAmount,
    string PriceCurrencyCode);