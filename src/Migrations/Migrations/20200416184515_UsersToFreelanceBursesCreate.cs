using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Migrations.Migrations
{
    public partial class UsersToFreelanceBursesCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "UsersToFreelanceBurses",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    FreelanceBurseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersToFreelanceBurses", x => x.Id);
                    table.ForeignKey(
                        "FK_UsersToFreelanceBurses_FreelanceBurses_FreelanceBurseId",
                        x => x.FreelanceBurseId,
                        "FreelanceBurses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_UsersToFreelanceBurses_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_UsersToFreelanceBurses_FreelanceBurseId",
                "UsersToFreelanceBurses",
                "FreelanceBurseId");

            migrationBuilder.CreateIndex(
                "IX_UsersToFreelanceBurses_UserId",
                "UsersToFreelanceBurses",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("UsersToFreelanceBurses");
        }
    }
}