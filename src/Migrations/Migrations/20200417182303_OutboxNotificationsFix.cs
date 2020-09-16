using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class OutboxNotificationsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Data",
                "OutboxNotifications",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "json");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Data",
                "OutboxNotifications",
                "json",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}