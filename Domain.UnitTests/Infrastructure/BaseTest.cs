using System.Net.Mail;
using Domain.Chores;
using Domain.UnitTests.Chores;
using Domain.Users;
using Shared;

namespace Domain.UnitTests.Infrastructure;

public abstract class BaseTest
{
    protected static User CreateTestUser()
    {
        return User.Create("John", "Doe", new MailAddress("johndoe@test.com"));
    }
    protected static Chore CreateTestChore()
    {
        Result<Chore> result = Chore.Create(
            ChoreData.UserId,
            ChoreData.ChoreListId,
            ChoreData.Name,
            ChoreData.Description,
            ChoreData.DayOfWeek,
            ChoreData.Priority,
            ChoreData.Frequency,
            ChoreData.Category);

        return result.Value;
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