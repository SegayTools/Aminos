using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V18_Remove_Rating_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaimaiDX_UserDetails_MaimaiDX_UserRatings_UserRatingId",
                table: "MaimaiDX_UserDetails");

            migrationBuilder.DropTable(
                name: "MaimaiDX_UserRates");

            migrationBuilder.DropTable(
                name: "MaimaiDX_UserRatings");

            migrationBuilder.DropTable(
                name: "MaimaiDX_UserUdemaes");

            migrationBuilder.DropIndex(
                name: "IX_MaimaiDX_UserDetails_UserRatingId",
                table: "MaimaiDX_UserDetails");

            migrationBuilder.DropColumn(
                name: "UserRatingId",
                table: "MaimaiDX_UserDetails");

            migrationBuilder.AddColumn<string>(
                name: "userRating",
                table: "MaimaiDX_UserDetails",
                type: "TEXT",
                maxLength: 8192,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userRating",
                table: "MaimaiDX_UserDetails");

            migrationBuilder.AddColumn<ulong>(
                name: "UserRatingId",
                table: "MaimaiDX_UserDetails",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MaimaiDX_UserUdemaes",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    classValue = table.Column<int>(type: "INTEGER", nullable: false),
                    loseNum = table.Column<uint>(type: "INTEGER", nullable: false),
                    maxClassValue = table.Column<int>(type: "INTEGER", nullable: false),
                    maxLoseNum = table.Column<uint>(type: "INTEGER", nullable: false),
                    maxRate = table.Column<int>(type: "INTEGER", nullable: false),
                    maxWinNum = table.Column<uint>(type: "INTEGER", nullable: false),
                    npcLoseNum = table.Column<uint>(type: "INTEGER", nullable: false),
                    npcMaxLoseNum = table.Column<uint>(type: "INTEGER", nullable: false),
                    npcMaxWinNum = table.Column<uint>(type: "INTEGER", nullable: false),
                    npcTotalLoseNum = table.Column<uint>(type: "INTEGER", nullable: false),
                    npcTotalWinNum = table.Column<uint>(type: "INTEGER", nullable: false),
                    npcWinNum = table.Column<uint>(type: "INTEGER", nullable: false),
                    rate = table.Column<int>(type: "INTEGER", nullable: false),
                    totalLoseNum = table.Column<uint>(type: "INTEGER", nullable: false),
                    totalWinNum = table.Column<uint>(type: "INTEGER", nullable: false),
                    winNum = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_UserUdemaes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaimaiDX_UserRatings",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    udemaeId = table.Column<ulong>(type: "INTEGER", nullable: true),
                    rating = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_UserRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaimaiDX_UserRatings_MaimaiDX_UserUdemaes_udemaeId",
                        column: x => x.udemaeId,
                        principalTable: "MaimaiDX_UserUdemaes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaimaiDX_UserRates",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserRatingId = table.Column<ulong>(type: "INTEGER", nullable: true),
                    UserRatingId1 = table.Column<ulong>(type: "INTEGER", nullable: true),
                    UserRatingId2 = table.Column<ulong>(type: "INTEGER", nullable: true),
                    UserRatingId3 = table.Column<ulong>(type: "INTEGER", nullable: true),
                    achievement = table.Column<uint>(type: "INTEGER", nullable: false),
                    level = table.Column<int>(type: "INTEGER", nullable: false),
                    musicId = table.Column<int>(type: "INTEGER", nullable: false),
                    romVersion = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_UserRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaimaiDX_UserRates_MaimaiDX_UserRatings_UserRatingId",
                        column: x => x.UserRatingId,
                        principalTable: "MaimaiDX_UserRatings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaimaiDX_UserRates_MaimaiDX_UserRatings_UserRatingId1",
                        column: x => x.UserRatingId1,
                        principalTable: "MaimaiDX_UserRatings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaimaiDX_UserRates_MaimaiDX_UserRatings_UserRatingId2",
                        column: x => x.UserRatingId2,
                        principalTable: "MaimaiDX_UserRatings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaimaiDX_UserRates_MaimaiDX_UserRatings_UserRatingId3",
                        column: x => x.UserRatingId3,
                        principalTable: "MaimaiDX_UserRatings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserDetails_UserRatingId",
                table: "MaimaiDX_UserDetails",
                column: "UserRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserRates_Id",
                table: "MaimaiDX_UserRates",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserRates_UserRatingId",
                table: "MaimaiDX_UserRates",
                column: "UserRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserRates_UserRatingId1",
                table: "MaimaiDX_UserRates",
                column: "UserRatingId1");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserRates_UserRatingId2",
                table: "MaimaiDX_UserRates",
                column: "UserRatingId2");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserRates_UserRatingId3",
                table: "MaimaiDX_UserRates",
                column: "UserRatingId3");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserRatings_Id",
                table: "MaimaiDX_UserRatings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserRatings_udemaeId",
                table: "MaimaiDX_UserRatings",
                column: "udemaeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserUdemaes_Id",
                table: "MaimaiDX_UserUdemaes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaimaiDX_UserDetails_MaimaiDX_UserRatings_UserRatingId",
                table: "MaimaiDX_UserDetails",
                column: "UserRatingId",
                principalTable: "MaimaiDX_UserRatings",
                principalColumn: "Id");
        }
    }
}
