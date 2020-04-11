using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class SubscriptionsCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:subscriptions", "email,vk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:subscriptions", "email,vk");
        }
    }
}
