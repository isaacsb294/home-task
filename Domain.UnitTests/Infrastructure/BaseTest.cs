using Domain.Chores;
using Domain.UnitTests.Chores;
using Shared;

namespace Domain.UnitTests.Infrastructure;

public abstract class BaseTest
{
    protected static Chore CreateTestChore()
    {
        return Chore.Create(
            ChoreData.Name,
            ChoreData.Description,
            ChoreData.Priority,
            ChoreData.Frequency,
            ChoreData.Category,
            ChoreData.DayOfWeek);
    }
    
    protected static T AssertDomainEventWasRaised<T>(Entity entity)
        where T : IDomainEvent
    {
        T? domainEvent = entity.DomainEvents
            .OfType<T>()
            .SingleOrDefault();

        if (domainEvent is null)
        {
            throw new Exception($"{typeof(T).Name} was not raised");
        }
        
        return domainEvent;
    }
}