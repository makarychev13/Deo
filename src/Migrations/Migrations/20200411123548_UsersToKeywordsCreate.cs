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
                "UsersToKeywords",
                table => new
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
                        "FK_UsersToKeywords_Keywords_KeywordId",
                        x => x.KeywordId,
                        "Keywords",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_UsersToKeywords_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_UsersToKeywords_KeywordId",
                "UsersToKeywords",
                "KeywordId");

            migrationBuilder.CreateIndex(
                "IX_UsersToKeywords_UserId",
                "UsersToKeywords",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("UsersToKeywords");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:subscriptions", "email,vk");
        }
    }
}