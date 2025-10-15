using Domain.Chores;
using Domain.ChoreUsers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ChoreUsers :  IEntityTypeConfiguration<ChoreUser>
{
    public void Configure(EntityTypeBuilder<ChoreUser> builder)
    {
        builder.ToTable("chore_users");
        
        builder.HasOne<Chore>()
            .WithMany()
            .HasForeignKey(cu => cu.ChoreId);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(cu => cu.UserId);
    }
}