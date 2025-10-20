using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace HomeTask.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        try
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();
            Console.WriteLine("Migration success");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Migration failed");
            Console.WriteLine(ex.Message);
        }
    }
}