using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V15_Add_DataTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CueName",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropColumn(
                name: "JacketFile",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropColumn(
                name: "MovieName",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropColumn(
                name: "NetOpenName",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropColumn(
                name: "RightFile",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.DropColumn(
                name: "ThumbnailName",
                table: "MaimaiDX_MusicDatas");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MaimaiDX_MusicDatas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MaimaiDX_EventDatas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "MaimaiDX_CharaDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Genre = table.Column<string>(type: "TEXT", nullable: true),
                    IsCopyright = table.Column<bool>(type: "INTEGER", nullable: false),
                    Disable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_CharaDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaimaiDX_FrameDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Genre = table.Column<string>(type: "TEXT", nullable: true),
                    NormText = table.Column<string>(type: "TEXT", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_FrameDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaimaiDX_IconDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Genre = table.Column<string>(type: "TEXT", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    NormText = table.Column<string>(type: "TEXT", nullable: true),
                    Disable = table.Column<bool>(type: "INTEGER", nullable: false),
                    EventNameId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_IconDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaimaiDX_IconDatas_MaimaiDX_EventDatas_EventNameId",
                        column: x => x.EventNameId,
                        principalTable: "MaimaiDX_EventDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaimaiDX_TitleDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Genre = table.Column<string>(type: "TEXT", nullable: true),
                    IsCopyright = table.Column<bool>(type: "INTEGER", nullable: false),
                    Disable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    RareType = table.Column<string>(type: "TEXT", nullable: true),
                    EventNameId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_TitleDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaimaiDX_TitleDatas_MaimaiDX_EventDatas_EventNameId",
                        column: x => x.EventNameId,
                        principalTable: "MaimaiDX_EventDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_CharaDatas_Id",
                table: "MaimaiDX_CharaDatas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_FrameDatas_Id",
                table: "MaimaiDX_FrameDatas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_IconDatas_EventNameId",
                table: "MaimaiDX_IconDatas",
                column: "EventNameId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_IconDatas_Id",
                table: "MaimaiDX_IconDatas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_TitleDatas_EventNameId",
                table: "MaimaiDX_TitleDatas",
                column: "EventNameId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_TitleDatas_Id",
                table: "MaimaiDX_TitleDatas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaimaiDX_CharaDatas");

            migrationBuilder.DropTable(
                name: "MaimaiDX_FrameDatas");

            migrationBuilder.DropTable(
                name: "MaimaiDX_IconDatas");

            migrationBuilder.DropTable(
                name: "MaimaiDX_TitleDatas");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MaimaiDX_MusicDatas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "CueName",
                table: "MaimaiDX_MusicDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JacketFile",
                table: "MaimaiDX_MusicDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MovieName",
                table: "MaimaiDX_MusicDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NetOpenName",
                table: "MaimaiDX_MusicDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RightFile",
                table: "MaimaiDX_MusicDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailName",
                table: "MaimaiDX_MusicDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MaimaiDX_EventDatas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
