using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V8_Add_Tables_For_Clients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client_Bookkeepings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    placeId = table.Column<int>(type: "INTEGER", nullable: false),
                    updateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    creditSetting0 = table.Column<int>(type: "INTEGER", nullable: false),
                    creditSetting1 = table.Column<int>(type: "INTEGER", nullable: false),
                    creditSetting2 = table.Column<int>(type: "INTEGER", nullable: false),
                    credits1P = table.Column<int>(type: "INTEGER", nullable: false),
                    credits2P = table.Column<int>(type: "INTEGER", nullable: false),
                    creditsFreedom = table.Column<int>(type: "INTEGER", nullable: false),
                    creditsTicket = table.Column<int>(type: "INTEGER", nullable: false),
                    creditCoin = table.Column<int>(type: "INTEGER", nullable: false),
                    creditService = table.Column<int>(type: "INTEGER", nullable: false),
                    creditEmoney = table.Column<int>(type: "INTEGER", nullable: false),
                    timeTotal = table.Column<int>(type: "INTEGER", nullable: false),
                    timeTotalPlay = table.Column<int>(type: "INTEGER", nullable: false),
                    timeLongest1P = table.Column<int>(type: "INTEGER", nullable: false),
                    timeShortest1P = table.Column<int>(type: "INTEGER", nullable: false),
                    timeLongest2P = table.Column<int>(type: "INTEGER", nullable: false),
                    timeShortest2P = table.Column<int>(type: "INTEGER", nullable: false),
                    timeLongestFreedom = table.Column<int>(type: "INTEGER", nullable: false),
                    timeShortestFreedom = table.Column<int>(type: "INTEGER", nullable: false),
                    newFreeUserNum = table.Column<int>(type: "INTEGER", nullable: false),
                    tutorialPlay = table.Column<int>(type: "INTEGER", nullable: false),
                    play1PNum = table.Column<int>(type: "INTEGER", nullable: false),
                    play2PNum = table.Column<int>(type: "INTEGER", nullable: false),
                    playFreedomNum = table.Column<int>(type: "INTEGER", nullable: false),
                    aimeLoginNum = table.Column<int>(type: "INTEGER", nullable: false),
                    guestLoginNum = table.Column<int>(type: "INTEGER", nullable: false),
                    regionId = table.Column<int>(type: "INTEGER", nullable: false),
                    playCount = table.Column<int>(type: "INTEGER", nullable: false),
                    coinToCredit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client_Bookkeepings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client_Settings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    placeId = table.Column<int>(type: "INTEGER", nullable: false),
                    placeName = table.Column<string>(type: "TEXT", nullable: true),
                    regionId = table.Column<int>(type: "INTEGER", nullable: false),
                    regionName = table.Column<string>(type: "TEXT", nullable: true),
                    bordId = table.Column<string>(type: "TEXT", nullable: true),
                    romVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    isDevelop = table.Column<bool>(type: "INTEGER", nullable: false),
                    isAou = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client_Testmodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    placeId = table.Column<int>(type: "INTEGER", nullable: false),
                    trackSingle = table.Column<int>(type: "INTEGER", nullable: false),
                    trackMulti = table.Column<int>(type: "INTEGER", nullable: false),
                    trackEvent = table.Column<int>(type: "INTEGER", nullable: false),
                    totalMachine = table.Column<int>(type: "INTEGER", nullable: false),
                    satelliteId = table.Column<int>(type: "INTEGER", nullable: false),
                    cameraPosition = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client_Testmodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_Bookkeepings_Id",
                table: "Client_Bookkeepings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Settings_Id",
                table: "Client_Settings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Testmodes_Id",
                table: "Client_Testmodes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client_Bookkeepings");

            migrationBuilder.DropTable(
                name: "Client_Settings");

            migrationBuilder.DropTable(
                name: "Client_Testmodes");
        }
    }
}
