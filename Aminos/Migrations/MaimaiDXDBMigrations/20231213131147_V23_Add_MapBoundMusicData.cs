using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V23_Add_MapBoundMusicData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaimaiDX_MapBoundMusicDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    MusicDatas = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_MapBoundMusicDatas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_MapBoundMusicDatas_Id",
                table: "MaimaiDX_MapBoundMusicDatas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaimaiDX_MapBoundMusicDatas");
        }
    }
}
