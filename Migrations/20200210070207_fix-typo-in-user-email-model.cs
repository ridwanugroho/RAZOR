using Microsoft.EntityFrameworkCore.Migrations;

namespace belajarRazor.Migrations
{
    public partial class fixtypoinuseremailmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emai",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "emai",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
