using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siemens.DAL.Migrations
{
    public partial class webUserTableUpdateRefreshtokencolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "WebUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenEndDate",
                table: "WebUsers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "WebUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenEndDate",
                table: "WebUsers");
        }
    }
}
