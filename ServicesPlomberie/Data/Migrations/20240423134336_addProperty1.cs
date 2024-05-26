using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesPlomberie.Data.Migrations
{
    public partial class addProperty1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_users_utilisateurId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_utilisateurId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "utilisateurId",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "souservice",
                schema: "security",
                table: "users",
                newName: "sousservice");

            migrationBuilder.AddColumn<int>(
                name: "serviceId",
                schema: "security",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

           

            migrationBuilder.AddForeignKey(
                name: "FK_users_Service_serviceId",
                schema: "security",
                table: "users",
                column: "serviceId",
                principalTable: "Service",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Service_serviceId",
                schema: "security",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_serviceId",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "serviceId",
                schema: "security",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "sousservice",
                schema: "security",
                table: "users",
                newName: "souservice");

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
    }
}
