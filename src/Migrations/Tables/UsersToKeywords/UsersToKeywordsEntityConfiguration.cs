using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Migrations.Tables.UsersToKeywords
{
    public class UsersToKeywordsEntityConfiguration : IEntityTypeConfiguration<UsersToKeywordsEntity>
    {
        public void Configure(EntityTypeBuilder<UsersToKeywordsEntity> builder)
        {
            builder.ToTable("UsersToKeywords");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.KeywordId).IsRequired();
            builder.Property(p => p.Include).IsRequired();

            builder
                .HasOne(p => p.User)
                .WithMany(p => p.ToKeywords)
                .HasForeignKey(p => p.UserId);

            builder
                .HasOne(p => p.Keyword)
                .WithMany(p => p.ToUsers)
                .HasForeignKey(p => p.KeywordId);
        }
    }
}