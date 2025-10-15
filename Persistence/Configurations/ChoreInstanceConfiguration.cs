using Domain.ChoreInstances;
using Domain.Chores;
using Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class ChoreInstanceConfiguration : IEntityTypeConfiguration<ChoreInstance>
{
    public void Configure(EntityTypeBuilder<ChoreInstance> builder)
    {
        builder.ToTable("chore_instances");
        
        builder.HasKey(ci => ci.Id);

        builder.HasOne<Chore>()
            .WithMany()
            .HasForeignKey(ci => ci.ChoreId)
            .IsRequired();
        
        builder.HasMany<Comment>()
            .WithOne()
            .HasForeignKey(comment => comment.ChoreInstanceId);
    }
}