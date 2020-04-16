using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Migrations.Migrations
{
    public partial class UsersToFreelanceBursesCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersToFreelanceBurses",
                columns: table => new
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
                        name: "FK_UsersToFreelanceBurses_FreelanceBurses_FreelanceBurseId",
                        column: x => x.FreelanceBurseId,
                        principalTable: "FreelanceBurses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersToFreelanceBurses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersToFreelanceBurses_FreelanceBurseId",
                table: "UsersToFreelanceBurses",
                column: "FreelanceBurseId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersToFreelanceBurses_UserId",
                table: "UsersToFreelanceBurses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersToFreelanceBurses");
        }
    }
}
