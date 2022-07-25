using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingZone.Data.Migrations
{
    public partial class PasswordPropertiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "tblUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "tblUsers",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "tblUsers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "tblUsers");
        }
    }
}
