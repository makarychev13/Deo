using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Migrations.Tables.Orders
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("Orders");
            
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Link).IsRequired();
            builder.Property(p => p.Publication).IsRequired();
            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion(
                    p => p.ToString(),
                    p => (ProcessingStatus)Enum.Parse(typeof(ProcessingStatus), p));
            builder.Property(p => p.FreelanceBurseId).IsRequired();
            builder
                .HasOne(p => p.FreelanceBurse)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.FreelanceBurseId);
        }
    }
}