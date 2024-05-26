using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Permissions;

#nullable disable

namespace ServicesPlomberie.Data.Migrations
{
    public partial class addPropertyForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserService",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserService", x => new { x.UserId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_UserService_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema:"security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserService_ServiceId",
                table: "UserService",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserService");
        }
    }
}
