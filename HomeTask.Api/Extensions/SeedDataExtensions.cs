using System.Net.Mail;
using Bogus;
using Domain.ChoreLists;
using Domain.Chores;
using Domain.Users;
using Persistence.Database;
using Shared;

namespace HomeTask.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        Console.WriteLine("Seeding database...");
        
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (dbContext.Users.Any())
        {
            Console.WriteLine("Database already seeded");
            return;
        }

        var faker = new Faker();
        Result<User> userResult = User.Create(
            faker.Person.FirstName,
            faker.Person.LastName,
            new MailAddress(faker.Person.Email));

        if (userResult.IsFailure)
        {
            throw new Exception("Unable to create user");
        }

        dbContext.Users.Add(userResult.Value);

        Result<ChoreList> choreListResult = ChoreList.Create(
            userResult.Value.Id,
            "Test Chore List",
            "A chore list to collect test data");

        if (choreListResult.IsFailure)
        {
            throw new Exception("Unable to create chore list");
        }

        dbContext.ChoreLists.Add(choreListResult.Value);

        for (var i = 0; i < 10; i++)
        {
            Result<Chore> result = Chore.Create(
                userResult.Value.Id,
                choreListResult.Value.Id,
                string.Join(" ", faker.Lorem.Words(5)),
                faker.Lorem.Lines(5),
                (DayOfWeek)Random.Shared.Next(1, 7),
                (ChorePriority)Random.Shared.Next(1, 3),
                (ChoreFrequency)Random.Shared.Next(1, 4),
                (ChoreCategory)Random.Shared.Next(1, 3));

            if (result.IsFailure)
            {
                continue;
            }
            
            dbContext.Chores.Add(result.Value);
        }
        
        dbContext.SaveChanges();
    }
}