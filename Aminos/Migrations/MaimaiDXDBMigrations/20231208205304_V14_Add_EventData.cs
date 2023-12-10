using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V14_Add_EventData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventName",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropColumn(
                name: "EventName2",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropColumn(
                name: "SubEventName",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.AddColumn<int>(
                name: "EventName2Id",
                table: "MaimaiDX_MusicDatas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventNameId",
                table: "MaimaiDX_MusicDatas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubEventNameId",
                table: "MaimaiDX_MusicDatas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MaimaiDX_EventDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    InfoType = table.Column<int>(type: "INTEGER", nullable: false),
                    AlwaysOpen = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_EventDatas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_MusicDatas_EventName2Id",
                table: "MaimaiDX_MusicDatas",
                column: "EventName2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_MusicDatas_EventNameId",
                table: "MaimaiDX_MusicDatas",
                column: "EventNameId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_MusicDatas_SubEventNameId",
                table: "MaimaiDX_MusicDatas",
                column: "SubEventNameId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_EventDatas_Id",
                table: "MaimaiDX_EventDatas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaimaiDX_MusicDatas_MaimaiDX_EventDatas_EventName2Id",
                table: "MaimaiDX_MusicDatas",
                column: "EventName2Id",
                principalTable: "MaimaiDX_EventDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaimaiDX_MusicDatas_MaimaiDX_EventDatas_EventNameId",
                table: "MaimaiDX_MusicDatas",
                column: "EventNameId",
                principalTable: "MaimaiDX_EventDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaimaiDX_MusicDatas_MaimaiDX_EventDatas_SubEventNameId",
                table: "MaimaiDX_MusicDatas",
                column: "SubEventNameId",
                principalTable: "MaimaiDX_EventDatas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaimaiDX_MusicDatas_MaimaiDX_EventDatas_EventName2Id",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_MaimaiDX_MusicDatas_MaimaiDX_EventDatas_EventNameId",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_MaimaiDX_MusicDatas_MaimaiDX_EventDatas_SubEventNameId",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropTable(
                name: "MaimaiDX_EventDatas");

            migrationBuilder.DropIndex(
                name: "IX_MaimaiDX_MusicDatas_EventName2Id",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropIndex(
                name: "IX_MaimaiDX_MusicDatas_EventNameId",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropIndex(
                name: "IX_MaimaiDX_MusicDatas_SubEventNameId",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropColumn(
                name: "EventName2Id",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropColumn(
                name: "EventNameId",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropColumn(
                name: "SubEventNameId",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.AddColumn<string>(
                name: "EventName",
                table: "MaimaiDX_MusicDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventName2",
                table: "MaimaiDX_MusicDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubEventName",
                table: "MaimaiDX_MusicDatas",
                type: "TEXT",
                nullable: true);
        }
    }
}
