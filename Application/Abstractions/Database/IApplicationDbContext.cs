using Domain.ChoreInstances;
using Domain.ChoreLists;
using Domain.Chores;
using Domain.ChoreUsers;
using Domain.Comments;
using Domain.Products;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Database;

public interface IApplicationDbContext
{
    DbSet<ChoreList> ChoreLists { get; }
    DbSet<Chore> Chores { get; }
    DbSet<ChoreUser> ChoreUsers { get; }
    DbSet<ChoreInstance> ChoreInstances { get; }
    DbSet<Comment> Comments { get; }
    DbSet<Product> Products { get; }
    DbSet<User> Users { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}