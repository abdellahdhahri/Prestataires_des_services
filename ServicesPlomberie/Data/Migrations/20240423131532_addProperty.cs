using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesPlomberie.Data.Migrations
{
    public partial class addProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "souservice",
                schema: "security",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "souservice",
                schema: "security",
                table: "users");
        }
    }
}
