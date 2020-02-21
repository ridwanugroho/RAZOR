using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace belajarRazor.Migrations
{
    public partial class createconversationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    uuid = table.Column<Guid>(nullable: false),
                    Fromid = table.Column<int>(nullable: true),
                    Toid = table.Column<int>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    SentTime = table.Column<DateTime>(nullable: false),
                    Read = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.uuid);
                    table.ForeignKey(
                        name: "FK_Conversations_User_Fromid",
                        column: x => x.Fromid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conversations_User_Toid",
                        column: x => x.Toid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_Fromid",
                table: "Conversations",
                column: "Fromid");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_Toid",
                table: "Conversations",
                column: "Toid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversations");
        }
    }
}
