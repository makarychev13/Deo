using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class OutboxNotificationsKeyUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "IdempotencyKey",
                "OutboxNotifications",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                "IX_OutboxNotifications_IdempotencyKey",
                "OutboxNotifications",
                "IdempotencyKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_OutboxNotifications_IdempotencyKey",
                "OutboxNotifications");

            migrationBuilder.AlterColumn<string>(
                "IdempotencyKey",
                "OutboxNotifications",
                "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}