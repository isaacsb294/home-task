using Application.Abstractions.Auth;
using FluentValidation;

namespace Application.Chores.RemoveProduct;

public class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
{
    public RemoveProductCommandValidator()
    {
        RuleFor(x => x.ChoreId).NotNull();
        RuleFor(x => x.ProductId).NotEmpty();
    }
}