using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Chores;
using Domain.Products;
using Shared;

namespace Application.Chores.AddProduct;

public class AddProductCommandHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext) : ICommandHandler<AddProductCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AddProductCommand command,
        CancellationToken cancellationToken = default)
    {
        Chore? chore = dbContext.Chores.FirstOrDefault(c => c.Id == command.ChoreId);

        if (chore is null || chore.UserId != userContext.UserId)
        {
            return Result.Failure<Guid>(ChoreErrors.NotFound);
        }

        var price = new Price(command.PriceAmount, Currency.FromCode(command.PriceCurrencyCode));

        Result<Product> result = chore.AddProduct(
            command.Name,
            command.Link,
            price);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        dbContext.Products.Add(result.Value);

        await dbContext.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}