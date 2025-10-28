using Application.Users;
using Domain.Chores;
using Domain.Products;

namespace Application.Chores;

public static class ChoreMapping
{

    private static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Link = product.Link.ToString(),
            PriceAmount = product.LastKnownPrice.Amount,
            PriceCurrencyCode = product.LastKnownPrice.Currency.Code
        };
    }
    
    public static ChoreDto ToDto(this Chore chore)
    {
        return new ChoreDto
        {
            Id = chore.Id,
            Name = chore.Name,
            Priority = chore.Priority,
            Frequency = chore.Frequency,
            Category = chore.Category,
            DayOfWeek = chore.DayOfWeek,
            Products = chore.Products.Select(p => p.ToDto()).ToList(),
            Assignees = chore.Assignees.Select(u => u.ToDto()).ToList()
        };
    }
}