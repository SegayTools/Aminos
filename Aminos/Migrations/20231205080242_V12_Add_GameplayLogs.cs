using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations
{
    /// <inheritdoc />
    public partial class V12_Add_GameplayLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "General.GameplayLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GameId = table.Column<string>(type: "TEXT", nullable: true),
                    UserAccountId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_General.GameplayLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_General.GameplayLogs_General.UserAccounts_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "General.UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_General.GameplayLogs_Id",
                table: "General.GameplayLogs",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_General.GameplayLogs_UserAccountId",
                table: "General.GameplayLogs",
                column: "UserAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "General.GameplayLogs");
        }
    }
}
