using Microsoft.EntityFrameworkCore.Migrations;

namespace belajarRazor.Migrations
{
    public partial class removesellerid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerID",
                table: "Purchases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SellerID",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
