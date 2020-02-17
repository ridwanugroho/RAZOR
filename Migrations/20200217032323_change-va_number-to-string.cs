using Microsoft.EntityFrameworkCore.Migrations;

namespace belajarRazor.Migrations
{
    public partial class changeva_numbertostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VaNumber",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDetailsid = table.Column<int>(type: "int", nullable: true),
                    bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    va_number = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
