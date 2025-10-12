using Domain.Products;
using Shared;

namespace Domain.Chores;

public class Chore : Entity
{
    private Chore()
    {
    }

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ChorePriority Priority { get; private set; }
    public ChoreFrequency Frequency { get; private set; }
    public bool IsCompleted { get; private set; } = false;
    public ChoreCategory? Category { get; private set; }
    public DayOfWeek? DayOfWeek { get; private set; }
    private readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products;

    public static Chore Create(
        string name,
        string description,
        ChorePriority? priority,
        ChoreFrequency? frequency,
        ChoreCategory? category,
        DayOfWeek? dayOfWeek)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException(nameof(description));
        }

        var chore = new Chore
        {
            Id = Guid.CreateVersion7(),
            Name = name,
            Description = description,
            Priority = priority ?? ChorePriority.Low,
            Frequency = frequency ?? ChoreFrequency.Daily,
            Category = category,
            DayOfWeek = dayOfWeek
        };
        
        chore.Raise(new ChoreCreatedDomainEvent(chore.Id));
        
        return chore;
    }

    public void AddProduct(Product product)
    {
        if (_products.Contains(product))
        {
            return;
        }
        
        _products.Add(product);
    }

    public void RemoveProduct(Product product)
    {
        if (!_products.Contains(product))
        {
            throw new InvalidOperationException("Product not found");
        }
        
        _products.Remove(product);
    }

    public void MarkComplete()
    {
        if (IsCompleted)
        {
            return;
        }
        
        IsCompleted = true;
        Raise(new ChoreCompletedDomainEvent(Id));
    }
    
    public void MarkIncomplete()
    {
        if (!IsCompleted)
        {
            return;
        }
        
        IsCompleted = false;
        Raise(new ChoreIncompleteDomainEvent(Id));
    }
}