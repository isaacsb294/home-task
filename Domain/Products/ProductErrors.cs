using Shared;

namespace Domain.Products;

public static class ProductErrors
{
    public static readonly Error BlankName = new(
        "Product.BlankName", 
        "The product's name cannot be blank.");
    
    public static readonly Error BlankLink = new(
        "Product.BlankLink", 
        "The product must have a relevant link.");
}