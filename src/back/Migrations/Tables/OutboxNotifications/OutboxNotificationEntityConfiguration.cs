using System;
using Domain.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Migrations.Tables.OutboxNotifications
{
    public class OutboxNotificationEntityConfiguration : IEntityTypeConfiguration<OutboxNotificationEntity>
    {
        public void Configure(EntityTypeBuilder<OutboxNotificationEntity> builder)
        {
            builder.ToTable("OutboxNotifications");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Data).IsRequired();
            
            builder.Property(p => p.Transport).IsRequired().HasConversion(
                p => p.ToString(),
                p => (Subscriptions)Enum.Parse(typeof(Subscriptions), p));

            builder.Property(p => p.IdempotencyKey).IsRequired();
            builder.HasIndex(p => p.IdempotencyKey).IsUnique();

            builder.Property(p => p.Status).IsRequired().HasConversion(
                p => p.ToString(),
                p => (OutboxNotificationsStatusEntity)Enum.Parse(typeof(OutboxNotificationsStatusEntity), p));
        }
    }
}