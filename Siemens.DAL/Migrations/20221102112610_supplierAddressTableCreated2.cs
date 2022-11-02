using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siemens.DAL.Migrations
{
    public partial class supplierAddressTableCreated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "SupplierAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "SupplierAddresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SupplierAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "SupplierAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "SupplierAddresses");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "SupplierAddresses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SupplierAddresses");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "SupplierAddresses");
        }
    }
}
