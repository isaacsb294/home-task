using System.Net.Mail;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => user.Id);
        
        builder.HasIndex(user => user.Email).IsUnique();
        
        builder.Property(user => user.Email)
            .HasConversion(
                email => email.ToString(), 
                email => new MailAddress(email));
        
        builder.Property(user => user.Email).HasMaxLength(320);
    }
}