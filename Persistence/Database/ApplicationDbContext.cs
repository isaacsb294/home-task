using Domain.ChoreInstances;
using Domain.ChoreLists;
using Domain.Chores;
using Domain.Comments;
using Domain.Products;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.DomainEvents;
using Shared;

namespace Persistence.Database;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IDomainEventsDispatcher domainEventsDispatcher) : DbContext(options)
{
    public DbSet<ChoreList> ChoreLists { get; set; }
    public DbSet<Chore> Chore { get; set; }
    public DbSet<ChoreInstance> ChoreInstances { get; set; }
    public DbSet<ChoreUserTag> ChoreUserTags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);
        await PublishDomainEventsAsync();
        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        List<IDomainEvent> domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(x => x.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = entity.DomainEvents;
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .ToList();
        
        await domainEventsDispatcher.DispatchAsync(domainEvents);
    }
}