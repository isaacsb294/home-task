using Domain.ChoreLists.Events;
using Domain.Chores;
using Shared;

namespace Domain.ChoreLists;

public class ChoreList : Entity
{
    private ChoreList()
    {
    }

    public Guid UserId { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static ChoreList Create(Guid userId, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException(nameof(description));
        }

        var choreList = new ChoreList
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
            Name = name,
            Description = description
        };

        choreList.Raise(new ChoreListCreatedDomainEvent(choreList.Id));

        return choreList;
    }
}