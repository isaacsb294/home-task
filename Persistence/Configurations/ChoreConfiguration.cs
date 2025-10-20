using Domain.ChoreLists;
using Domain.Chores;
using Domain.ChoreUsers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class ChoreConfiguration : IEntityTypeConfiguration<Chore>
{
    public void Configure(EntityTypeBuilder<Chore> builder)
    {
        builder.ToTable("chores");

        builder.HasKey(chore => chore.Id);

        builder.HasMany(chore => chore.Products)
            .WithOne()
            .HasForeignKey(product => product.ChoreId);
        
        builder.HasOne<ChoreList>()
            .WithMany()
            .HasForeignKey(c => c.ChoreListId);

        builder.HasMany(chore => chore.Assignees)
            .WithMany()
            .UsingEntity<ChoreUser>();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(chore => chore.UserId)
            .IsRequired();
    }
}