using Domain.ChoreLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class ChoreListConfiguration : IEntityTypeConfiguration<ChoreList>
{
    public void Configure(EntityTypeBuilder<ChoreList> builder)
    {
        builder.ToTable("chore_lists");
        builder.HasKey(cl => cl.Id);
    }
}