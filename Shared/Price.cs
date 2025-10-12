namespace Shared;

public record Price
{
    public static Price Zero => new(0, Currency.Gbp);
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }
    
    public Price(decimal amount, Currency currency)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        Amount = amount;
        Currency = currency;
    }

    public static Price operator +(Price p1, Price p2)
    {
        if (p1.Currency != p2.Currency)
        {
            throw new InvalidOperationException("Currency must match");
        }
        
        return new Price(p1.Amount + p2.Amount, p1.Currency);
    }

    public static Price operator -(Price p1, Price p2)
    {
        if (p1.Currency != p2.Currency)
        {
            throw new InvalidOperationException("Currency must match");
        }
        
        decimal amount = p1.Amount - p2.Amount;

        if (amount < 0)
        {
            amount = 0;
        }
        
        return new Price(amount, p1.Currency);
    }
};