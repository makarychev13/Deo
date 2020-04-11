using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Migrations.Migrations
{
    public partial class UsersToKeywordsCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:subscriptions", "email,vk");

            migrationBuilder.CreateTable(
                name: "UsersToKeywords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    KeywordId = table.Column<int>(nullable: false),
                    Include = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersToKeywords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersToKeywords_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersToKeywords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersToKeywords_KeywordId",
                table: "UsersToKeywords",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersToKeywords_UserId",
                table: "UsersToKeywords",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersToKeywords");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:subscriptions", "email,vk");
        }
    }
}
