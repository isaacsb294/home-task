using Domain.Chores.Events;
using Domain.Products;
using Domain.Users;
using Shared;

namespace Domain.Chores;

public class Chore : Entity
{
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
    public List<Product> Products { get; init; } = [];
    public List<User> ResponsiblePersons { get; init; } = [];

    public static Chore Create(
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
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException(nameof(description));
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
}