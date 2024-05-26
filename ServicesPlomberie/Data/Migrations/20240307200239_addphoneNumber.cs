﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesPlomberie.Data.Migrations
{
    public partial class addphoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "picture",
                schema: "security",
                table: "users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "picture",
                schema: "security",
                table: "users");
        }
    }
}