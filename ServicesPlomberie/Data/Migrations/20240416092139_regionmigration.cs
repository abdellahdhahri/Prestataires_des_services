using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesPlomberie.Data.Migrations
{
    public partial class regionmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "region",
                schema: "security",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "utilisateurId",
                table: "Service",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_utilisateurId",
                table: "Service",
                column: "utilisateurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_users_utilisateurId",
                table: "Service",
                column: "utilisateurId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_users_utilisateurId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_utilisateurId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "region",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "utilisateurId",
                table: "Service");
        }
    }
}
