using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class OrdersLastModificationsDateFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                "LastModificationDate",
                "Orders",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 4, 24, 18, 10, 42, 660, DateTimeKind.Local).AddTicks(6930));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                "LastModificationDate",
                "Orders",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 24, 18, 10, 42, 660, DateTimeKind.Local).AddTicks(6930),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "now()");
        }
    }
}