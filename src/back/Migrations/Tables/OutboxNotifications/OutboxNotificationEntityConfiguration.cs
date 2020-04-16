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
        }
    }
}