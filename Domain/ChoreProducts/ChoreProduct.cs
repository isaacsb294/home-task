using Domain.Chores;

namespace Domain.ChoreProducts;

public class ChoreProduct
{
    public Guid ChoreId { get; init; }
    public Guid ProductId { get; init; }
}