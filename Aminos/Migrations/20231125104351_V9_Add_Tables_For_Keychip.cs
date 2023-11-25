using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations
{
    /// <inheritdoc />
    public partial class V9_Add_Tables_For_Keychip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "General.Cards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "General.UserAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastLoginWebDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_General.UserAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "General.Keychips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Enable = table.Column<bool>(type: "INTEGER", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastAccessDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_General.Keychips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_General.Keychips_General.UserAccounts_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "General.UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_General.Cards_UserAccountId",
                table: "General.Cards",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_General.Keychips_OwnerId",
                table: "General.Keychips",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_General.Cards_General.UserAccounts_UserAccountId",
                table: "General.Cards",
                column: "UserAccountId",
                principalTable: "General.UserAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_General.Cards_General.UserAccounts_UserAccountId",
                table: "General.Cards");

            migrationBuilder.DropTable(
                name: "General.Keychips");

            migrationBuilder.DropTable(
                name: "General.UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_General.Cards_UserAccountId",
                table: "General.Cards");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "General.Cards");
        }
    }
}
