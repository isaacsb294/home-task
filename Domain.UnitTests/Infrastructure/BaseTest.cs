using Shared;

namespace Domain.UnitTests.Infrastructure;

public abstract class BaseTest
{
    public static T AssertDomainEventWasRaised<T>(Entity entity)
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