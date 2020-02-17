using Microsoft.EntityFrameworkCore.Migrations;

namespace belajarRazor.Migrations
{
    public partial class changeva_numberandactiontopublic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_actions",
                table: "TransactionDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_va_numbers",
                table: "TransactionDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_ItemsDetail",
                table: "Purchases",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_actions",
                table: "TransactionDetail");

            migrationBuilder.DropColumn(
                name: "_va_numbers",
                table: "TransactionDetail");

            migrationBuilder.DropColumn(
                name: "_ItemsDetail",
                table: "Purchases");
        }
    }
}
