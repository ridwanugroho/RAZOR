using Microsoft.EntityFrameworkCore.Migrations;

namespace belajarRazor.Migrations
{
    public partial class addbillkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bill_key",
                table: "TransactionDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "biller_code",
                table: "TransactionDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bill_key",
                table: "TransactionDetail");

            migrationBuilder.DropColumn(
                name: "biller_code",
                table: "TransactionDetail");
        }
    }
}
