using Domain.Chores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class ChoreListConfiguration : IEntityTypeConfiguration<Chore>
{
    public void Configure(EntityTypeBuilder<Chore> builder)
    {
        builder.ToTable("chore_lists");
        
        builder.HasKey(cl => cl.Id);

        builder.HasMany<Chore>()
            .WithOne()
            .HasForeignKey(chore => chore.ChoreListId);
    }
}