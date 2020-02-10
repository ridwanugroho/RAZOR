using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace belajarRazor.Migrations
{
    public partial class addcreatedandupdatetimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Cart",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "editedAt",
                table: "Cart",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Barang",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "editedAt",
                table: "Barang",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "editedAt",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Barang");

            migrationBuilder.DropColumn(
                name: "editedAt",
                table: "Barang");
        }
    }
}
