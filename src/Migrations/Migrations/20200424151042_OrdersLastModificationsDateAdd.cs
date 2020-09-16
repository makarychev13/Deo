using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class OrdersLastModificationsDateAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                "LastModificationDate",
                "Orders",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 24, 18, 10, 42, 660, DateTimeKind.Local).AddTicks(6930));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "LastModificationDate",
                "Orders");
        }
    }
}