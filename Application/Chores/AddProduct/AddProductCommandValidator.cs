using FluentValidation;
using Shared;

namespace Application.Chores.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Link).NotEmpty();
        RuleFor(x => x.PriceAmount).NotEmpty().GreaterThan(0);
        
        RuleFor(x => x.PriceCurrencyCode)
            .Must(code => Currency.All.FirstOrDefault(x => x.Code == code) is not null)
            .WithMessage("Invalid currency code");
    }
}