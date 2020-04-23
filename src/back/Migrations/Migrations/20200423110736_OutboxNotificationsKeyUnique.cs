using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class OutboxNotificationsKeyUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdempotencyKey",
                table: "OutboxNotifications",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxNotifications_IdempotencyKey",
                table: "OutboxNotifications",
                column: "IdempotencyKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxNotifications_IdempotencyKey",
                table: "OutboxNotifications");

            migrationBuilder.AlterColumn<string>(
                name: "IdempotencyKey",
                table: "OutboxNotifications",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
