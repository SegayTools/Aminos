using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V24_Add_PlateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaimaiDX_PlateDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Genre = table.Column<string>(type: "TEXT", nullable: true),
                    Disable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    NormText = table.Column<string>(type: "TEXT", nullable: true),
                    EventNameId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_PlateDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaimaiDX_PlateDatas_MaimaiDX_EventDatas_EventNameId",
                        column: x => x.EventNameId,
                        principalTable: "MaimaiDX_EventDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_PlateDatas_EventNameId",
                table: "MaimaiDX_PlateDatas",
                column: "EventNameId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_PlateDatas_Id",
                table: "MaimaiDX_PlateDatas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaimaiDX_PlateDatas");
        }
    }
}
