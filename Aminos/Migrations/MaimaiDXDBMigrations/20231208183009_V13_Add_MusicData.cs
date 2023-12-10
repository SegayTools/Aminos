using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V13_Add_MusicData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaimaiDX_MusicDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Artist = table.Column<string>(type: "TEXT", nullable: true),
                    RightsInfoName = table.Column<string>(type: "TEXT", nullable: true),
                    ReleaseTagName = table.Column<string>(type: "TEXT", nullable: true),
                    NetOpenName = table.Column<string>(type: "TEXT", nullable: true),
                    SortName = table.Column<string>(type: "TEXT", nullable: true),
                    GenreName = table.Column<string>(type: "TEXT", nullable: true),
                    Bpm = table.Column<float>(type: "REAL", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    AddVersion = table.Column<string>(type: "TEXT", nullable: true),
                    MovieName = table.Column<string>(type: "TEXT", nullable: true),
                    CueName = table.Column<string>(type: "TEXT", nullable: true),
                    Dresscode = table.Column<bool>(type: "INTEGER", nullable: false),
                    EventName = table.Column<string>(type: "TEXT", nullable: true),
                    EventName2 = table.Column<string>(type: "TEXT", nullable: true),
                    SubEventName = table.Column<string>(type: "TEXT", nullable: true),
                    LockType = table.Column<int>(type: "INTEGER", nullable: false),
                    SubLockType = table.Column<int>(type: "INTEGER", nullable: false),
                    DotNetListView = table.Column<bool>(type: "INTEGER", nullable: false),
                    UtageKanjiName = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    UtagePlayStyle = table.Column<int>(type: "INTEGER", nullable: false),
                    JacketFile = table.Column<string>(type: "TEXT", nullable: true),
                    ThumbnailName = table.Column<string>(type: "TEXT", nullable: true),
                    RightFile = table.Column<string>(type: "TEXT", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    NotesData = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: true),
                    FixedOptions = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_MusicDatas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_MusicDatas_Id",
                table: "MaimaiDX_MusicDatas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaimaiDX_MusicDatas");
        }
    }
}
