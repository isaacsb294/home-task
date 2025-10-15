using Domain.Chores.Events;
using Domain.Products;
using Domain.Users;
using Shared;

namespace Domain.Chores;

public class Chore : Entity
{
    private readonly List<Product> _products = [];
    private readonly List<User> _responsiblePersons = [];
    
    private Chore()
    {
    }

    public Guid UserId { get; private set; }
    public Guid ChoreListId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ChorePriority Priority { get; private set; }
    public ChoreFrequency Frequency { get; private set; }
    public ChoreCategory? Category { get; private set; }
    public DayOfWeek? DayOfWeek { get; private set; }
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    public IReadOnlyCollection<User> ResponsiblePersons => _responsiblePersons.AsReadOnly();

    public static Result<Chore> Create(
        Guid userId,
        Guid choreListId,
        string name,
        string description,
        ChorePriority? priority,
        ChoreFrequency? frequency,
        ChoreCategory? category,
        DayOfWeek? dayOfWeek)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Chore>(ChoreErrors.BlankName);
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure<Chore>(ChoreErrors.BlankDescription);
        }

        var chore = new Chore
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
            ChoreListId = choreListId,
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

    public Result AddProduct(
        string name,
        string link,
        Price lastKnownPrice)
    {
        Result<Product> result = Product.Create(
            Id,
            name,
            link,
            lastKnownPrice);

        if (result.IsFailure)
        {
            return result;
        }
        
        _products.Add(result.Value);
        
        Raise(new ChoreProductAddedDomainEvent(Id, result.Value.Id));
        
        return Result.Success();
    }

    public Result RemoveProduct(Guid productId)
    {
        Product? toRemove = _products.FirstOrDefault(p => p.Id == productId);
        
        if (toRemove is null)
        {
            return Result.Failure(ChoreErrors.ProductNotExists);
        }
        
        _products.Remove(toRemove);
        
        Raise(new ChoreProductRemovedDomainEvent(Id, productId));
        
        return Result.Success();
    }

    public Result AddResponsiblePerson(User user)
    {
        if (_responsiblePersons.FirstOrDefault(p => p.Id == user.Id) is not null)
        {
            return Result.Failure(ChoreErrors.UserExists);
        }
        
        _responsiblePersons.Add(user);
        
        Raise(new ChoreUserAddedDomainEvent(Id, user.Id));
        
        return Result.Success();
    }

    public Result RemoveResponsiblePerson(Guid userId)
    {
        User? toRemove = _responsiblePersons.FirstOrDefault(p => p.Id == userId);
        
        if (toRemove is  null)
        {
            return Result.Failure(ChoreErrors.UserNotExists);
        }
        
        _responsiblePersons.Remove(toRemove);
        
        Raise(new ChoreUserRemovedDomainEvent(Id, userId));
        
        return Result.Success();
    }
}