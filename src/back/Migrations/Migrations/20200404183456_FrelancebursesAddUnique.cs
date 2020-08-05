using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class FrelancebursesAddUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                "IX_FreelanceBurses_Link",
                "FreelanceBurses",
                "Link",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_FreelanceBurses_Name",
                "FreelanceBurses",
                "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_FreelanceBurses_Link",
                "FreelanceBurses");

            migrationBuilder.DropIndex(
                "IX_FreelanceBurses_Name",
                "FreelanceBurses");
        }
    }
}