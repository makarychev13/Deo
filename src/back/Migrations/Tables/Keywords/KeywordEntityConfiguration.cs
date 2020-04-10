using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Migrations.Tables.Keywords
{
    public class KeywordEntityConfiguration : IEntityTypeConfiguration<KeywordEntity>
    {
        public void Configure(EntityTypeBuilder<KeywordEntity> builder)
        {
            builder.ToTable("Keywords");
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).IsRequired();
        }
    }
}