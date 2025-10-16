using Application.Abstractions.Auth;
using Application.Abstractions.Database;
using Application.Abstractions.Messaging;
using Domain.Chores;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Chores.RemoveProduct;

public class RemoveProductCommandHandler(
    IApplicationDbContext dbContext,
    IUserContext userContext) : ICommandHandler<RemoveProductCommand>
{
    public async Task<Result> HandleAsync(RemoveProductCommand command, CancellationToken cancellationToken = default)
    {
        Chore? chore = await dbContext.Chores
            .FirstOrDefaultAsync(c => c.Id == command.ChoreId, cancellationToken);

        if (chore is null || chore.UserId != userContext.UserId)
        {
            return Result.Failure(ChoreErrors.NotFound);
        }
        
        Product? product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ChoreErrors.ProductNotExists);
        }
        
        Result result = chore.RemoveProduct(product.Id);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }
        
        dbContext.Products.Remove(product);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}