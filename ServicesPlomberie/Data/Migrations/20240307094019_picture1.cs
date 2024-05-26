using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesPlomberie.Data.Migrations
{
    public partial class picture1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pictue",
                schema: "security",
                table: "users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "pictue",
                schema: "security",
                table: "users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
