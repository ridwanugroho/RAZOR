using Microsoft.EntityFrameworkCore.Migrations;

namespace belajarRazor.Migrations
{
    public partial class addsellerID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SellerID",
                table: "Purchases",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerID",
                table: "Purchases");
        }
    }
}
