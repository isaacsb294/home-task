using Domain.Comments;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments");
        builder.HasKey(comment => comment.Id);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(comment => comment.UserId);
    }
}