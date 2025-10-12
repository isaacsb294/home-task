using Domain.Chores;
using Shared;

namespace Domain.ChoreLists;

public class ChoreList : Entity
{
    private ChoreList()
    {
    }
    
    private readonly List<Chore> _chores = [];
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public IReadOnlyCollection<Chore> Chores => _chores.AsReadOnly();

    public static ChoreList Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException(nameof(description));
        }
        
        var taskList = new ChoreList
        {
            Id = Guid.CreateVersion7(),
            Name = name,
            Description = description
        };
        
        taskList.Raise(new ChoreListCreatedDomainEvent(taskList.Id));
        
        return taskList;
    }

    public void AddChore(Chore chore)
    {
        if (_chores.FirstOrDefault(t => t.Id == chore.Id) is not null)
        {
            throw new InvalidOperationException($"Task {chore.Id} already exists");
        }
        
        _chores.Add(chore);

        Raise(new ChoreAddedToListDomainEvent(Id, chore.Id));
    }

    public void RemoveChore(Chore chore)
    {
        if (_chores.FirstOrDefault(t => t.Id == chore.Id) is null)
        {
            throw new InvalidOperationException($"Task {chore.Id} does not exist");
        }
        
        _chores.Remove(chore);
        
        Raise(new ChoreRemovedFromListDomainEvent(Id, chore.Id));
    }
}