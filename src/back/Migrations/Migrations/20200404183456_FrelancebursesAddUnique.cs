using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class FrelancebursesAddUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FreelanceBurses_Link",
                table: "FreelanceBurses",
                column: "Link",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FreelanceBurses_Name",
                table: "FreelanceBurses",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FreelanceBurses_Link",
                table: "FreelanceBurses");

            migrationBuilder.DropIndex(
                name: "IX_FreelanceBurses_Name",
                table: "FreelanceBurses");
        }
    }
}
