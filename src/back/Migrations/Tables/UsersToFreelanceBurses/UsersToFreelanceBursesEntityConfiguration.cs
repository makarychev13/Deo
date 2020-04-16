using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Migrations.Tables.UsersToFreelanceBurses
{
    public class UsersToFreelanceBursesEntityConfiguration : IEntityTypeConfiguration<UsersToFreelanceBursesEntity>
    {
        public void Configure(EntityTypeBuilder<UsersToFreelanceBursesEntity> builder)
        {
            builder.ToTable("UsersToFreelanceBurses");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.FreelanceBurseId).IsRequired();

            builder
                .HasOne(p => p.User)
                .WithMany(p => p.ToFreelanceBurses)
                .HasForeignKey(p => p.UserId);

            builder
                .HasOne(p => p.FreelanceBurse)
                .WithMany(p => p.ToUsers)
                .HasForeignKey(p => p.FreelanceBurseId);
        }
    }
}