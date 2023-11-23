using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V3_Fix_SDEZ_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "playlogId",
                table: "MaimaiDX_UserGamePlaylogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<int>(
                name: "point",
                table: "MaimaiDX_UserFriendSeasonRankings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "rank",
                table: "MaimaiDX_UserFriendSeasonRankings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "recordDate",
                table: "MaimaiDX_UserFriendSeasonRankings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "rewardGet",
                table: "MaimaiDX_UserFriendSeasonRankings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "seasonId",
                table: "MaimaiDX_UserFriendSeasonRankings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "MaimaiDX_UserFriendSeasonRankings",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "playlogId",
                table: "MaimaiDX_UserGamePlaylogs");

            migrationBuilder.DropColumn(
                name: "point",
                table: "MaimaiDX_UserFriendSeasonRankings");

            migrationBuilder.DropColumn(
                name: "rank",
                table: "MaimaiDX_UserFriendSeasonRankings");

            migrationBuilder.DropColumn(
                name: "recordDate",
                table: "MaimaiDX_UserFriendSeasonRankings");

            migrationBuilder.DropColumn(
                name: "rewardGet",
                table: "MaimaiDX_UserFriendSeasonRankings");

            migrationBuilder.DropColumn(
                name: "seasonId",
                table: "MaimaiDX_UserFriendSeasonRankings");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "MaimaiDX_UserFriendSeasonRankings");
        }
    }
}
