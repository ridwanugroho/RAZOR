using Microsoft.EntityFrameworkCore.Migrations;

namespace belajarRazor.Migrations
{
    public partial class adduseridtobarang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Barang",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Barang");
        }
    }
}
