using Domain.Products;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;

namespace Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(product => product.Id);

        builder.OwnsOne(product => product.LastKnownPrice, priceBuilder =>
        {
            priceBuilder.Property(price => price.Currency)
                .HasMaxLength(3)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.Property(product => product.Link).HasConversion(
            uri => uri.ToString(),
            value => new Uri(value));
    }
}