using System.Linq;
using Domain.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Migrations.Tables.Users
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Email).IsRequired();
            builder.HasIndex(p => p.Email).IsUnique();

            builder.Property(p => p.PasswordHash).IsRequired();

            builder.Property(p => p.Subscriptions).IsRequired();
        }
    }
}