using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Migrations.Tables.FreelanceBurses
{
    public class FreelanceBurseEntityConfiguration : IEntityTypeConfiguration<FreelanceBurseEntity>
    {
        public void Configure(EntityTypeBuilder<FreelanceBurseEntity> builder)
        {
            builder.ToTable("FreelanceBurses");
            
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Link).IsRequired();
            builder.Property(p => p.Name).IsRequired();
        }
    }
}