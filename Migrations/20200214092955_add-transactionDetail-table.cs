using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace belajarRazor.Migrations
{
    public partial class addtransactionDetailtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionDetail",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status_code = table.Column<string>(nullable: true),
                    status_message = table.Column<string>(nullable: true),
                    transaction_id = table.Column<string>(nullable: true),
                    order_id = table.Column<string>(nullable: true),
                    merchant_id = table.Column<string>(nullable: true),
                    gross_amount = table.Column<string>(nullable: true),
                    currency = table.Column<string>(nullable: true),
                    payment_type = table.Column<string>(nullable: true),
                    transaction_time = table.Column<DateTime>(nullable: false),
                    transaction_status = table.Column<string>(nullable: true),
                    fraud_status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "VaNumber",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bank = table.Column<string>(nullable: true),
                    va_number = table.Column<string>(nullable: true),
                    TransactionDetailsid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaNumber", x => x.id);
                    table.ForeignKey(
                        name: "FK_VaNumber_TransactionDetail_TransactionDetailsid",
                        column: x => x.TransactionDetailsid,
                        principalTable: "TransactionDetail",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaNumber_TransactionDetailsid",
                table: "VaNumber",
                column: "TransactionDetailsid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaNumber");

            migrationBuilder.DropTable(
                name: "TransactionDetail");
        }
    }
}
