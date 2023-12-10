using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations
{
    /// <inheritdoc />
    public partial class V11_Add_Table_AnnAnnouncements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastPlayDate",
                table: "General.UserAccounts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "General.Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    UserAccountId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_General.Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_General.Activities_General.UserAccounts_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "General.UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "General.Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    UserAccountId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_General.Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_General.Announcements_General.UserAccounts_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "General.UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_General.Activities_Id",
                table: "General.Activities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_General.Activities_UserAccountId",
                table: "General.Activities",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_General.Announcements_Id",
                table: "General.Announcements",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_General.Announcements_UserAccountId",
                table: "General.Announcements",
                column: "UserAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "General.Activities");

            migrationBuilder.DropTable(
                name: "General.Announcements");

            migrationBuilder.DropColumn(
                name: "LastPlayDate",
                table: "General.UserAccounts");
        }
    }
}
